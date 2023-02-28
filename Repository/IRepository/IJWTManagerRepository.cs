using HMS_API.Models;
using HMS_API.Models.Dto;

namespace HMS_API.Repository.IRepository
{
    public interface IJWTManagerRepository
    {
        Task<Tokens> AuthenticateAsync(LoginViewDto users);
    }
}
