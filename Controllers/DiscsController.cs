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
        public async Task<IEnumerable<Disc>> GetDiscs()
        {
            return await _discRepository.Get();
        }
   
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Disc>> GetDiscs(int id)
        {
            return await _discRepository.Get(id);
        }

        [Route("get/{name}")]
        [HttpGet]
        public async Task<ActionResult<Disc>> GetDisc(string name)
        {
            return await _discRepository.GetDisc(name);
        }




        [HttpPost]
        public async Task<ActionResult<Disc>>PostDiscs([FromBody] Disc disc) // Model binding converts json in request payload to (disc) object
        {
            var newDisc = await _discRepository.Create(disc);
            return CreatedAtAction(nameof(GetDiscs), new { id = newDisc.Id }, newDisc);
        }

        [HttpPut]
        public async Task<ActionResult> PutDiscs(int id, [FromBody] Disc disc)
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
