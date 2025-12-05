using Microsoft.AspNetCore.Mvc;
using Library.Common;
using Library.Infrastructure.Models;
using Library.REST.Models;

namespace Library.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EBooksController : ControllerBase
    {
        private readonly ICrudServiceAsync<EBookModel> _ebookService;

        public EBooksController(ICrudServiceAsync<EBookModel> ebookService)
        {
            _ebookService = ebookService;
        }

        // GET: api/ebooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EBookDto>>> GetEBooks()
        {
            var ebooks = await _ebookService.ReadAllAsync();
            var ebookDtos = ebooks.Select(e => new EBookDto
            {
                Id = e.Id,
                Title = e.Title,
                Author = e.Author,
                Year = e.Year,
                Pages = e.Pages,
                Format = e.Format,
                FileSizeMB = e.FileSizeMB
            });
            return Ok(ebookDtos);
        }

        // GET: api/ebooks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EBookDto>> GetEBook(Guid id)
        {
            var ebook = await _ebookService.ReadAsync(id);
            if (ebook == null)
            {
                return NotFound();
            }

            var ebookDto = new EBookDto
            {
                Id = ebook.Id,
                Title = ebook.Title,
                Author = ebook.Author,
                Year = ebook.Year,
                Pages = ebook.Pages,
                Format = ebook.Format,
                FileSizeMB = ebook.FileSizeMB
            };
            return Ok(ebookDto);
        }

        // POST: api/ebooks
        [HttpPost]
        public async Task<ActionResult<EBookDto>> CreateEBook(CreateEBookDto createEBookDto)
        {
            var ebook = new EBookModel
            {
                Id = Guid.NewGuid(),
                Title = createEBookDto.Title,
                Author = createEBookDto.Author,
                Year = createEBookDto.Year,
                Pages = createEBookDto.Pages,
                Format = createEBookDto.Format,
                FileSizeMB = createEBookDto.FileSizeMB
            };

            var result = await _ebookService.CreateAsync(ebook);
            if (result)
            {
                await _ebookService.SaveAsync();
                var ebookDto = new EBookDto
                {
                    Id = ebook.Id,
                    Title = ebook.Title,
                    Author = ebook.Author,
                    Year = ebook.Year,
                    Pages = ebook.Pages,
                    Format = ebook.Format,
                    FileSizeMB = ebook.FileSizeMB
                };
                return CreatedAtAction(nameof(GetEBook), new { id = ebook.Id }, ebookDto);
            }

            return BadRequest();
        }

        // DELETE: api/ebooks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEBook(Guid id)
        {
            var ebook = await _ebookService.ReadAsync(id);
            if (ebook == null)
            {
                return NotFound();
            }

            var result = await _ebookService.RemoveAsync(ebook);
            if (result)
            {
                await _ebookService.SaveAsync();
                return NoContent();
            }

            return BadRequest();
        }
    }
}