using Microsoft.AspNetCore.Mvc;
using StatsApi.Models;
using StatsApi.Services;

namespace StatsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsItemsController : ControllerBase
    {
        private readonly ScoreService _scoreService;

        public StatsItemsController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        // GET: api/StatsItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScores()
        {
            var scores = await _scoreService.GetAllAsync();
            return Ok(scores);
        }

        // GET: api/StatsItems/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Score>> GetScore(string id)
        {
            var score = await _scoreService.GetByIdAsync(id);

            if (score is null)
                return NotFound();

            return Ok(score);
        }

        // POST: api/StatsItems
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            await _scoreService.CreateAsync(score);
            return CreatedAtAction(nameof(GetScore), new { id = score.Id }, score);
        }

        // PUT: api/StatsItems/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> PutScore(string id, Score score)
        {
            var existing = await _scoreService.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            score.Id = id;
            await _scoreService.UpdateAsync(id, score);
            return NoContent();
        }

        // DELETE: api/StatsItems/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteScore(string id)
        {
            var score = await _scoreService.GetByIdAsync(id);
            if (score is null)
                return NotFound();

            await _scoreService.DeleteAsync(id);
            return NoContent();
        }
    }
}
