using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using WebApiUser.Data;
using WebApiUser.Models.Requests;
using WebApiUser.Models.Responses;

namespace WebApiUser.Services
{
    public class LoginService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _token;

        public LoginService(AppDbContext context, TokenService token)
        {
            _context = context;
            _token = token;
        }

        public async Task<ServiceResponse<string>> Authenticate(LoginRequest request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstAsync(x => x.Email == request.Email && x.Password == request.Password);
            try
            {
                if (user is null)
                    throw new Exception("Credenciais de usuário não encontradas.");
                response.Data = _token.Generate(user);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Login efetuado com sucesso! Utilize seu token para se autenticar.";
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao gerar o token! Confira a mensagem de erro.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = ex.Message;
            }

            return response;
        }

    }
}
