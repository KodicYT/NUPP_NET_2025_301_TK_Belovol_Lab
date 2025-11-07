using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Library.Common;
using Library.Infrastructure.Models;
using Library.REST.Models;

namespace Library.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ICrudServiceAsync<BookModel> _bookService;

        public BooksController(ICrudServiceAsync<BookModel> bookService)
        {
            _bookService = bookService;
        }

        // GET: api/books - Public access
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.ReadAllAsync();
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Year = b.Year,
                Pages = b.Pages
            });
            return Ok(bookDtos);
        }

        // GET: api/books/{id} - Public access
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookDto>> GetBook(Guid id)
        {
            var book = await _bookService.ReadAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Pages = book.Pages
            };
            return Ok(bookDto);
        }

        // POST: api/books - Only Librarian and Admin
        [HttpPost]
        [Authorize(Policy = "RequireLibrarianRole")]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            var book = new BookModel
            {
                Id = Guid.NewGuid(),
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                Year = createBookDto.Year,
                Pages = createBookDto.Pages
            };

            var result = await _bookService.CreateAsync(book);
            if (result)
            {
                await _bookService.SaveAsync();
                var bookDto = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year,
                    Pages = book.Pages
                };
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
            }

            return BadRequest();
        }

        // PUT: api/books/{id} - Only Librarian and Admin
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireLibrarianRole")]
        public async Task<IActionResult> UpdateBook(Guid id, CreateBookDto updateBookDto)
        {
            var existingBook = await _bookService.ReadAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = updateBookDto.Title;
            existingBook.Author = updateBookDto.Author;
            existingBook.Year = updateBookDto.Year;
            existingBook.Pages = updateBookDto.Pages;

            var result = await _bookService.UpdateAsync(existingBook);
            if (result)
            {
                await _bookService.SaveAsync();
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/books/{id} - Only Admin
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bookService.ReadAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var result = await _bookService.RemoveAsync(book);
            if (result)
            {
                await _bookService.SaveAsync();
                return NoContent();
            }

            return BadRequest();
        }
    }
}