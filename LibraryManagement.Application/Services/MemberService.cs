using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepo;

    public MemberService(IMemberRepository memberRepo)
    {
        _memberRepo = memberRepo;
    }

    public async Task<List<Member>> GetAllMembersAsync()
        => await _memberRepo.GetAllAsync();

    public async Task<Member?> GetMemberByIdAsync(Guid id)
        => await _memberRepo.GetByIdAsync(id);

    public async Task<(bool IsSuccess, Member? Member, string? Error)> AddMemberAsync(AddMemberDto dto)
    {
        var exists = await _memberRepo.ExistsByEmailOrPhoneAsync(dto.Email, dto.Phone);
        if (exists)
            return (false, null, "A member with the same email or phone already exists.");
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var member = new Member
        {
            MemberId = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = hashedPassword
            
        };

        await _memberRepo.AddAsync(member);
        return (true, member, null);
    }

    public async Task<(bool IsSuccess, Member? Member)> UpdateMemberAsync(Guid id, AddMemberDto dto)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return (false, null);

        member.FullName = dto.FullName;
        member.Email = dto.Email;
        member.Phone = dto.Phone;

        await _memberRepo.UpdateAsync(member);
        return (true, member);
    }

    public async Task<bool> DeleteMemberAsync(Guid id)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return false;

        await _memberRepo.DeleteAsync(member);
        return true;
    }

    public async Task<Member?> GetMemberByPhone(string phone)
    {
        var member = await _memberRepo.GetByPhoneAsync(phone);

        return member;
    }
}
