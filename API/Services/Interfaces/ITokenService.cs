using API.Entities;

namespace API.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}