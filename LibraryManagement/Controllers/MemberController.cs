using LibraryManagement.Infrastructure;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMembers()
    {
        var members = await _memberService.GetAllMembersAsync();
        return Ok(members);
    }

    [HttpGet("getByPhone")]
    public async Task<IActionResult> GetByPhone(string number)
    {
        var member = await _memberService.GetMemberByPhone(number);
        return Ok(member);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMember(Guid id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> AddMember(AddMemberDto dto)
    {
        var result = await _memberService.AddMemberAsync(dto);
        return result.IsSuccess ? Ok(result.Member) : BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMember(Guid id, AddMemberDto dto)
    {
        var result = await _memberService.UpdateMemberAsync(id, dto);
        return result.IsSuccess ? Ok(result.Member) : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMember(Guid id)
    {
        var isDeleted = await _memberService.DeleteMemberAsync(id);
        return isDeleted ? Ok(new { message = "Member deleted successfully." }) : NotFound();
    }
}

