using DiscsAPI.Models;
using DiscsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscsController : ControllerBase
    {
        private readonly IDiscRepository _discRepository;
        public DiscsController(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Disc>> GetAll()
        {
            return await _discRepository.Get();
        }
   
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Disc>> Get(int id)
        {
            return await _discRepository.Get(id);
        }

        [Route("get/{name}")]
        [HttpGet]
        public async Task<ActionResult<Disc>> Get(string name)
        {
            return await _discRepository.Get(name);
        }

        // Post an array of disc objects
        [Route("array")]
        [HttpPost]
        public async Task<ActionResult<Disc>> Post([FromBody] Disc[] discs) 
        {
            var newDiscs = await _discRepository.Create(discs);
            return Created("Created discs:", newDiscs);
        }

        // Post single disc
        [HttpPost]
        public async Task<ActionResult<Disc>>Post([FromBody] Disc disc) // [From Body] Model binding converts json in request payload to (disc) object
        {
            var newDisc = await _discRepository.Create(disc);
            return CreatedAtAction(nameof(Get), new { id = newDisc.Id }, newDisc);
        }

        [HttpPut]
        public async Task<ActionResult> PutDisc(int id, [FromBody] Disc disc)
        {
            if (id != disc.Id) return BadRequest();

            await _discRepository.Update(disc);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var discToDelete = await _discRepository.Get(id);
            if (discToDelete == null) return NotFound();

            await _discRepository.Delete(discToDelete.Id);
            return NoContent();

        }
       
    }
}
