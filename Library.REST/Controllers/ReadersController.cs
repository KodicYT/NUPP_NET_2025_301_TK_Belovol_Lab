using Microsoft.AspNetCore.Mvc;
using Library.Common;
using Library.Infrastructure.Models;
using Library.REST.Models;

namespace Library.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : ControllerBase
    {
        private readonly ICrudServiceAsync<ReaderModel> _readerService;

        public ReadersController(ICrudServiceAsync<ReaderModel> readerService)
        {
            _readerService = readerService;
        }

        // GET: api/readers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReaderDto>>> GetReaders()
        {
            var readers = await _readerService.ReadAllAsync();
            var readerDtos = readers.Select(r => new ReaderDto
            {
                Id = r.Id,
                Name = r.Name,
                BooksBorrowed = r.BooksBorrowed
            });
            return Ok(readerDtos);
        }

        // GET: api/readers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReaderDto>> GetReader(Guid id)
        {
            var reader = await _readerService.ReadAsync(id);
            if (reader == null)
            {
                return NotFound();
            }

            var readerDto = new ReaderDto
            {
                Id = reader.Id,
                Name = reader.Name,
                BooksBorrowed = reader.BooksBorrowed
            };
            return Ok(readerDto);
        }

        // POST: api/readers
        [HttpPost]
        public async Task<ActionResult<ReaderDto>> CreateReader(CreateReaderDto createReaderDto)
        {
            var reader = new ReaderModel
            {
                Id = Guid.NewGuid(),
                Name = createReaderDto.Name,
                BooksBorrowed = createReaderDto.BooksBorrowed
            };

            var result = await _readerService.CreateAsync(reader);
            if (result)
            {
                await _readerService.SaveAsync();
                var readerDto = new ReaderDto
                {
                    Id = reader.Id,
                    Name = reader.Name,
                    BooksBorrowed = reader.BooksBorrowed
                };
                return CreatedAtAction(nameof(GetReader), new { id = reader.Id }, readerDto);
            }

            return BadRequest();
        }

        // PUT: api/readers/{id}/borrow
        [HttpPut("{id}/borrow")]
        public async Task<IActionResult> BorrowBook(Guid id)
        {
            var reader = await _readerService.ReadAsync(id);
            if (reader == null)
            {
                return NotFound();
            }

            reader.BooksBorrowed++;
            var result = await _readerService.UpdateAsync(reader);
            if (result)
            {
                await _readerService.SaveAsync();
                return Ok(new { message = "Book borrowed successfully", booksBorrowed = reader.BooksBorrowed });
            }

            return BadRequest();
        }

        // DELETE: api/readers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReader(Guid id)
        {
            var reader = await _readerService.ReadAsync(id);
            if (reader == null)
            {
                return NotFound();
            }

            var result = await _readerService.RemoveAsync(reader);
            if (result)
            {
                await _readerService.SaveAsync();
                return NoContent();
            }

            return BadRequest();
        }
    }
}