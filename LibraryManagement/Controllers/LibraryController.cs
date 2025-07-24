using LibraryManagement.Application.DTOs.Library;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLibraries()
        {
            var result = await _libraryService.GetLibrariesAsync();
            return Ok(result);
        }

        [HttpGet("/GetAllLibrariesName")]
        public async Task<IActionResult> GetAllLibrariesName()
        {
            var result = await _libraryService.GetLibraryNamesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibraryById(Guid id)
        {
            var result = await _libraryService.GetLibraryByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLibrary(AddLibraryDto addLibrary)
        {
            var message = await _libraryService.CreateLibraryAsync(addLibrary);
            return Ok(new { message });
        }
    }
}
