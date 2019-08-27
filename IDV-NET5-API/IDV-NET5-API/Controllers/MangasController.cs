using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MangaAPI.Data;
using MangaAPI.Models;
using IDV_NET5_API.Models;

namespace MangaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangasController : ControllerBase
    {
        private readonly MangaAPIContext _context;

        public MangasController(MangaAPIContext context)
        {
            _context = context;
        }

        private bool SearchQuery(Manga m, SearchParam param)
        {
            return (param.Price == 0 || param.Price == m.Price)
                && (param.Year == 0 || param.Year == m.Year)
                && (string.IsNullOrEmpty(param.Title) || param.Title == m.Title)
                && (string.IsNullOrEmpty(param.Genre) || param.Genre == m.Genre);
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Manga>>> SearchManga([FromQuery] SearchParam param)
        {

            int nbResult = param.nbResult == 0 ? 5 : param.nbResult;
            int pageIndex = param.PageIndex == 0 ? 1 : param.PageIndex;
            List<Manga> mangas = await _context.Mangas.Where(m => SearchQuery(m, param)).ToListAsync();
            if (mangas.Count() == 0)
                return mangas;
            int from = (mangas.Count() > pageIndex * nbResult) ?
                pageIndex * nbResult :
                (mangas.Count() - nbResult >= 0) ?
                mangas.Count() - nbResult :
                0;
            if (mangas.Count() < from + nbResult)
                return mangas;
            return mangas.GetRange(from, nbResult);
        }

        // GET: api/Mangas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangas()
        {
            return await _context.Mangas.ToListAsync();
        }

        // GET: api/Mangas/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Manga>> GetManga(int id)
        {
            var manga = await _context.Mangas.FindAsync(id);

            if (manga == null)
            {
                return NotFound();
            }

            return manga;
        }

        // PUT: api/Mangas/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutManga(int id, Manga manga)
        {
            if (id != manga.Id)
            {
                return BadRequest();
            }

            _context.Entry(manga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MangaExists(id))
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

        // POST: api/Mangas
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Manga>> PostManga(Manga manga)
        {
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManga", new { id = manga.Id }, manga);
        }

        // DELETE: api/Mangas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteManga(int id)
        {
            var manga = await _context.Mangas.FindAsync(id);
            if (manga == null)
            {
                return false;
            }

            _context.Mangas.Remove(manga);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool MangaExists(int id)
        {
            return _context.Mangas.Any(e => e.Id == id);
        }

        /// GET: api/mangas/5/Comments
        [HttpGet("{id}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByMangaId(int id)
        {
            return await _context.Comments.Where(c => c.MangaId == id).ToListAsync();
        }

        // GET: api/mangas/5/marks
        [HttpGet("{id}/marks")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetMarksByMangaId(int id)
        {
            return await _context.Ratings.Where(r => r.MangaId == id).ToListAsync();
        }
    }
}
