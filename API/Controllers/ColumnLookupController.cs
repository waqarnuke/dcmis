using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ColumnLookupController : BaseApiController
    {
        private readonly IColumnLookupRepository _repo;

        public ColumnLookupController(IColumnLookupRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<ColumnLookup>>> GetAll()
        {
            var columnLookups = await _repo.GetAsync();
            return Ok(columnLookups);
        }
        
        [HttpGet("GetAllByUserId")]
        public async Task<ActionResult<IReadOnlyList<ColumnLookup>>> GetAllByUserId(string userId)
        {
            var columnLookups = await _repo.GetAsync(x => x.UserId == userId);
            if (columnLookups == null) return NotFound();
            return Ok(columnLookups);
        }

        [HttpGet("GetByUserId")]
        public async Task<ActionResult<ColumnLookup>> GetById(string id)
        {
            var columnLookup = await _repo.GetByIdAsync(x => x.UserId == id);
            return Ok(columnLookup);
        }
        [HttpPost]
        public async Task<ActionResult<ColumnLookup>> Create(ColumnLookup obj)
        {
            _repo.Add(obj);
            if (await _repo.SaveChangesAsync())
                return Ok(obj);
            return BadRequest("Failed to create new column lookup");
        }
        [HttpPut]
        public async Task<ActionResult<ColumnLookup>> Update(ColumnLookup obj)
        {
            var columnLookup = await _repo.GetByIdAsync(x => x.Id == obj.Id);
            if (columnLookup == null) return NotFound();
            _repo.Update(obj);
            if (await _repo.SaveChangesAsync())
                return Ok(obj);
            return BadRequest("Failed to update column lookup");
        }
        
        [HttpDelete("{id}")]    
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var columnLookup = await _repo.GetByIdAsync(x => x.Id == id);
            if (columnLookup == null) return NotFound();
            _repo.Delete(columnLookup);
            if (await _repo.SaveChangesAsync())
                return Ok(true);
            return BadRequest("Failed to delete column lookup");
        }
    }
}
