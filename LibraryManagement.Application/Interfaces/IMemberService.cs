using LibraryManagement.Model;
using LibraryManagement.Model.Entities;

public interface IMemberService
{
    Task<List<Member>> GetAllMembersAsync();
    Task<Member?> GetMemberByIdAsync(Guid id);
    Task<(bool IsSuccess, Member? Member, string? Error)> AddMemberAsync(AddMemberDto dto);
    Task<(bool IsSuccess, Member? Member)> UpdateMemberAsync(Guid id, AddMemberDto dto);
    Task<bool> DeleteMemberAsync(Guid id);
    Task<Member?> GetMemberByPhone(string phone); 
}
