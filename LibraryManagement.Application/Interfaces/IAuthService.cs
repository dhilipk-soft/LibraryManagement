using LibraryManagement.Application.DTOs.Log;

namespace LibraryManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
