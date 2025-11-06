using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RulesApi.Data;
using RulesApi.Models;

namespace RulesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RulesController : ControllerBase
    {
        private readonly RulesDbContext _db;
        public RulesController(RulesDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rule>>> Get()
        {
            var rules = await _db.Rules.OrderBy(r => r.Priority).ToListAsync();
            return Ok(rules);
        }

        public class RulePriorityDto
        {
            public int Id { get; set; }
            public int Priority { get; set; }
        }

        public class ReorderRequest
        {
            public List<RulePriorityDto> Rules { get; set; } = new();
        }

        [HttpPut("reorder")]
        public async Task<IActionResult> Reorder([FromBody] ReorderRequest request)
        {
            if (request.Rules == null || request.Rules.Count == 0)
                return BadRequest("No rules provided");

            var dbRules = await _db.Rules.ToListAsync();
            var map = request.Rules.ToDictionary(r => r.Id, r => r.Priority);

            foreach (var rule in dbRules)
            {
                if (map.TryGetValue(rule.Id, out var newPriority))
                {
                    rule.Priority = newPriority;
                }
            }

            // normalize 1..N
            var normalized = dbRules.OrderBy(r => r.Priority).ToList();
            for (int i = 0; i < normalized.Count; i++)
            {
                normalized[i].Priority = i + 1;
            }

            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
