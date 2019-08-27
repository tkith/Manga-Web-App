using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MangaAPI.Data;
using MangaAPI.Models;

namespace MangaAPI.Controllers
{
    [Route("api/marks")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly MangaAPIContext _context;

        public RatingsController(MangaAPIContext context)
        {
            _context = context;
        }


        // GET: api/marks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetMarks()
        {
            return await _context.Ratings.ToListAsync();
        }

        // GET: api/marks/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Rating>> GetMark(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        // PUT: api/marks/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutMarks(int id, Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/marks
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Rating>> PostMarks(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMark", new { id = rating.Id }, rating);
        }

        // DELETE: api/marks/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Rating>> DeleteMarks(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return rating;
        }

        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }

        // GET: api/marks/userIdMangaId?userId=2&mangaId=2
        [HttpGet("userIdMangaId")]
        public async Task <ActionResult <Rating>> IsMarksExistByUserIdAndMangaId(int userId, int mangaId)
        {
            var rating = await _context.Ratings.Where(r => r.UserId == userId && r.MangaId == mangaId).FirstOrDefaultAsync();

            if (rating != null)
                return rating;
            return null;
        }
    }
}
