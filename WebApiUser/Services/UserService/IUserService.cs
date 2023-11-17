using WebApiUser.Models;
using WebApiUser.Models.Requests;
using WebApiUser.Models.Responses;

namespace WebApiUser.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserResponse>>> GetUserList(int offset, int limit);
        Task<ServiceResponse<UserResponse>> GetUserById(int id);
        Task<ServiceResponse<UserResponse>> GetUserByEmail(string email);
        Task<ServiceResponse<UserResponse>> CreateUser(CreateUserRequest request);
        Task<ServiceResponse<User>> UpdateNameUser(UpdateUserNameRequest request);
        Task<ServiceResponse<User>> UpdateEmailUser(UpdateUserEmailRequest request);
        Task<ServiceResponse<User>> UpdatePasswordUser(UpdateUserPasswordRequest request);
        Task<ServiceResponse<User>> DeleteUser(int id);
    }
}
