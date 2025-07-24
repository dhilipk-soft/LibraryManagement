using LibraryManagement.Model.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Member member);
        string GenerateRefreshToken();
    }
}
