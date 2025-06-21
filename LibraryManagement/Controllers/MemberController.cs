using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly LibraryContext dbContext;

        public MemberController(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllMembers()
        {
            return Ok(dbContext.Members.ToList());
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetMember(Guid id)
        {
            var member = dbContext.Members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpPost]
        public IActionResult AddMember(AddMemberDto dto)
        {

            var existingMember = dbContext.Members
            .FirstOrDefault(m => m.Email == dto.Email || m.Phone == dto.Phone);

            if (existingMember != null)
            {
                return BadRequest("A member with the same email or phone already exists.");
            }
            // Optional: check for existing email/phone if needed
            var member = new Member
            {
                MemberId = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone
            };

            dbContext.Members.Add(member);
            dbContext.SaveChanges();

            return Ok(member);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateMember(Guid id, AddMemberDto dto)
        {
            var existing = dbContext.Members.FirstOrDefault(m => m.MemberId == id);
            if (existing == null)
                return NotFound();

            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;

            dbContext.SaveChanges();
            return Ok(existing);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteMember(Guid id)
        {
            var member = dbContext.Members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
                return NotFound();

            dbContext.Members.Remove(member);
            dbContext.SaveChanges();
            return Ok("Member deleted successfully.");
        }
    }
}
