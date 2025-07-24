using LibraryManagement.Application.DTOs.Log;
using LibraryManagement.Application.Interfaces;
using System;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IMemberRepository memberRepository, IJwtService jwtService)
    {
        _memberRepository = memberRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var member = await _memberRepository.GetByPhoneAsync(request.Phone);

        if (member == null || string.IsNullOrEmpty(member.PasswordHash) ||
            !BCrypt.Net.BCrypt.Verify(request.Password, member.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid phone number or password");
        }

        var accessToken = _jwtService.GenerateAccessToken(member);
        var refreshToken = _jwtService.GenerateRefreshToken();

        await _memberRepository.UpdateRefreshTokenAsync(member, refreshToken, DateTime.UtcNow.AddDays(7));

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15)
        };
    }

}
