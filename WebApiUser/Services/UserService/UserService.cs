using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using WebApiUser.Data;
using WebApiUser.Models;
using WebApiUser.Models.Requests;
using WebApiUser.Models.Responses;
using WebApiUser.Validators;

namespace WebApiUser.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<UserResponse>>> GetUserList(int offset, int limit)
        {

            var response = new ServiceResponse<List<UserResponse>>();
            var userResponseList = new List<UserResponse>();

            foreach (User user in await _context.Users.Skip(offset).Take(limit).ToListAsync())
            {
                userResponseList.Add(CreateUserResponse(user));
            }
            response.Data = userResponseList;
            response.Message = "Listagem de todos os usuários";
            response.StatusCode = HttpStatusCode.OK;


            return response;
        }

        public async Task<ServiceResponse<UserResponse>> GetUserById(int id)
        {
            var response = new ServiceResponse<UserResponse>();

            try
            {
                var user = FindUserAsync(id).Result;

                response.Data = CreateUserResponse(user);
                response.Message = "Usuário encontrado!";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao encontrar o usuário! Confira a mensagem de erro.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<UserResponse>> GetUserByEmail(string email)
        {
            var response = new ServiceResponse<UserResponse>();

            try
            {
                var user = await _context.Users.FirstAsync(x => x.Email == email) ?? throw new Exception("Email de usuário não encontrado");

                response.Data = CreateUserResponse(user);
                response.Message = "Usuário encontrado!";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao encontrar o usuário! Confira a mensagem de erro.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<UserResponse>> CreateUser(CreateUserRequest request)
        {
            var response = new ServiceResponse<UserResponse>();

            string error = UserValidator.Validate(request.Name, request.Email, request.Password);

            try
            {
                if (!error.IsNullOrEmpty())
                    throw new Exception(error);

                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = request.Password,
                };

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                var findUser = FindUserAsync(user.Id).Result;

                response.Data = CreateUserResponse(findUser);
                response.Message = "Usuário cadastrado com sucesso!";
                response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao cadastrar o usuário! Confira a mensagem de erro.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<User>> UpdateNameUser(UpdateUserNameRequest request)
        {
            var response = new ServiceResponse<User>();
            try
            {
                string error = UserValidator.ValidateName(request.Name);

                if (!error.IsNullOrEmpty())
                    throw new Exception(error);

                var user = FindUserAsync(request.Id).Result;

                user.Name = request.Name;
                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Message = $"Nome de usuário alterado para: {user.Name}";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao alterar o usuário! Confira a mensagem de erro.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ex.Message;
            }

            return response;
        }
        public async Task<ServiceResponse<User>> UpdateEmailUser(UpdateUserEmailRequest request)
        {
            var response = new ServiceResponse<User>();
            try
            {
                string error = UserValidator.ValidateEmail(request.Email);

                if (!error.IsNullOrEmpty())
                    throw new Exception(error);

                var user = FindUserAsync(request.Id).Result;

                user.Email = request.Email;
                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Message = $"Email de usuário alterado para: {user.Email}";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao alterar o usuário! Confira a mensagem de erro.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ex.Message;
            }

            return response;
        }
        public async Task<ServiceResponse<User>> UpdatePasswordUser(UpdateUserPasswordRequest request)
        {
            var response = new ServiceResponse<User>();
            try
            {
                string error = UserValidator.ValidatePassword(request.Password);

                if (!error.IsNullOrEmpty())
                    throw new Exception(error);

                var user = FindUserAsync(request.Id).Result;

                user.Password = request.Password;
                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Message = $"Senha de usuário alterada";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao alterar o usuário! Confira a mensagem de erro.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<User>> DeleteUser(int id)
        {
            var response = new ServiceResponse<User>();

            try
            {
                var user = FindUserAsync(id).Result;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                response.Message = "Usuário excluído com sucesso";
                response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao excluir o usuário! Confira a mensagem de erro.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ex.Message;
            }

            return response;
        }

        private UserResponse CreateUserResponse(User user)
        {
            var userResponse = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return userResponse;
        }

        private async Task<User> FindUserAsync(int id)
        {
            return await _context.Users.FirstAsync(x => x.Id == id) ?? throw new Exception("Id de usuário não encontrado");
        }
    }
}
