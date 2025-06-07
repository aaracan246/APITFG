using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatsApi.Models;

namespace StatsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsItemsController : ControllerBase
    {
        private readonly ScoreContext _context;

        public StatsItemsController(ScoreContext context)
        {
            _context = context;
        }

        // GET: api/StatsItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScore()
        {
            return await _context.Score.ToListAsync();
        }

        // GET: api/StatsItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(long id)
        {
            var score = await _context.Score.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }

        // PUT: api/StatsItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(long id, Score score)
        {
            if (id != score.Id)
            {
                return BadRequest();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StatsItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            _context.Score.Add(score);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetScore", new { id = score.Id }, score);
            return CreatedAtAction(nameof(GetScore), new { id = score.Id }, score);
        }

        // DELETE: api/StatsItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(long id)
        {
            var score = await _context.Score.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            _context.Score.Remove(score);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScoreExists(long id)
        {
            return _context.Score.Any(e => e.Id == id);
        }
    }
}
