using LibraryManagement.Infrastructure;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryContext dbContext;
        public LibraryController(LibraryContext dbContext) {
            this.dbContext = dbContext;
        }

        // GET: api/Library
        [HttpGet]
        public IActionResult GetAllLibraries()
        {
            var libraries = dbContext.Libraries
                .Include(l => l.LibraryMembers)
                    .ThenInclude(lm => lm.Member)
                .Select(l => new    
                {
                    LibraryName = l.LibraryName,
                    LibraryId = l.LibraryId,
                    Members = l.LibraryMembers.Select(member => new LoanMemberDto
                    {
                        MemberId = member.MemberId,
                        FullName = member.Member.FullName,
                        Email = member.Member.Email,
                        Phone = member.Member.Phone
                    }).ToList()
                }).ToList();
            return Ok(libraries);
        }

        // GET: api/Library/{id}
        [HttpGet("{id}")]
        public IActionResult GetLibraryById(Guid id)
        {

            var libraries = dbContext.Libraries
                .Where(k => k.LibraryId == id)
                .Include(l => l.LibraryMembers)
                    .ThenInclude(lm => lm.Member)
                    .Select(l => new LibraryDto
                    {
                        LibraryName = l.LibraryName,
                        LibraryId = l.LibraryId,
                        Members = l.LibraryMembers.Select(member => new LoanMemberDto
                        {
                            MemberId = member.MemberId,
                            FullName = member.Member.FullName,
                            Email = member.Member.Email,
                            Phone = member.Member.Phone
                        }).ToList()
                    }).FirstOrDefault();

            if (libraries == null)
            {
                return NotFound();
            }


            return Ok(libraries);
        }

        //POST: api/Library
        [HttpPost]
        public async Task<IActionResult> CreateLibrary(AddLibraryDto addLibrary)
        {
            var library = new Library
            {
                LibraryId = Guid.NewGuid(),
                LibraryName = addLibrary.LibraryName
            };

            var validMembersId = await dbContext.Members
                .Where(m => addLibrary.MembersIds.Contains(m.MemberId))
                .Select(m => m.MemberId)
                .ToListAsync();

            var libraryMembers = validMembersId.Select(memberId => new LibraryMembers
            {
                LibraryId = library.LibraryId,
                MemberId = memberId
            }).ToList();

            dbContext.Libraries.Add(library);
            dbContext.LibraryMembers.AddRange(libraryMembers);

            await dbContext.SaveChangesAsync();

            return Ok(new {message = "Library and member added successfully" });
        }
    }
}
