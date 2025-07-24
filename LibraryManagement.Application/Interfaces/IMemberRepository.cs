using LibraryManagement.Model.Entities;

public interface IMemberRepository 
{
    Task<Member?> GetByIdAsync(Guid id);
    Task<List<Member>> GetAllAsync();
    Task<bool> ExistsByEmailOrPhoneAsync(string email, string phone);
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
    Task DeleteAsync(Member member);
    Task<Member?> GetByPhoneAsync(string phone);
    Task UpdateRefreshTokenAsync(Member member, string refreshToken, DateTime expiryTime);
}
