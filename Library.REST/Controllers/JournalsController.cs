using Microsoft.AspNetCore.Mvc;
using Library.Common;
using Library.Infrastructure.Models;
using Library.REST.Models;

namespace Library.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalsController : ControllerBase
    {
        private readonly ICrudServiceAsync<JournalModel> _journalService;

        public JournalsController(ICrudServiceAsync<JournalModel> journalService)
        {
            _journalService = journalService;
        }

        // GET: api/journals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JournalDto>>> GetJournals()
        {
            var journals = await _journalService.ReadAllAsync();
            var journalDtos = journals.Select(j => new JournalDto
            {
                Id = j.Id,
                Title = j.Title,
                Year = j.Year,
                IssueNumber = j.IssueNumber,
                Publisher = j.Publisher
            });
            return Ok(journalDtos);
        }

        // GET: api/journals/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JournalDto>> GetJournal(Guid id)
        {
            var journal = await _journalService.ReadAsync(id);
            if (journal == null)
            {
                return NotFound();
            }

            var journalDto = new JournalDto
            {
                Id = journal.Id,
                Title = journal.Title,
                Year = journal.Year,
                IssueNumber = journal.IssueNumber,
                Publisher = journal.Publisher
            };
            return Ok(journalDto);
        }

        // POST: api/journals
        [HttpPost]
        public async Task<ActionResult<JournalDto>> CreateJournal(CreateJournalDto createJournalDto)
        {
            var journal = new JournalModel
            {
                Id = Guid.NewGuid(),
                Title = createJournalDto.Title,
                Year = createJournalDto.Year,
                IssueNumber = createJournalDto.IssueNumber,
                Publisher = createJournalDto.Publisher
            };

            var result = await _journalService.CreateAsync(journal);
            if (result)
            {
                await _journalService.SaveAsync();
                var journalDto = new JournalDto
                {
                    Id = journal.Id,
                    Title = journal.Title,
                    Year = journal.Year,
                    IssueNumber = journal.IssueNumber,
                    Publisher = journal.Publisher
                };
                return CreatedAtAction(nameof(GetJournal), new { id = journal.Id }, journalDto);
            }

            return BadRequest();
        }

        // DELETE: api/journals/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJournal(Guid id)
        {
            var journal = await _journalService.ReadAsync(id);
            if (journal == null)
            {
                return NotFound();
            }

            var result = await _journalService.RemoveAsync(journal);
            if (result)
            {
                await _journalService.SaveAsync();
                return NoContent();
            }

            return BadRequest();
        }
    }
}