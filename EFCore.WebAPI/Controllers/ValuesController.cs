using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly HeroiContext _context;
        public ValuesController(HeroiContext context)
        {
            _context = context;
        }
        //GET: api/<ValuesController>
        [HttpGet("filtro/{nome}")]
        public ActionResult<IEnumerable<string>> GetFiltro(string nome)
        {
           var listHeroi = _context.Herois
                             .Where (h => h.Nome.Contains(nome))
                             .ToList();

           //var listHeroi = (from heroi in _context.Herois
            //                where heroi.Nome.Contains(nome)
            //                select heroi).ToList();
            return Ok(listHeroi);
        }

        // GET api/<ValuesController>/5
        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            //var heroi = new Heroi { Nome = nameHero };
            var heroi = _context.Herois
                             .Where(h => h.Id == 5)
                             .FirstOrDefault();
            heroi.Nome = "Homem Aranha";
               // _context.Herois.Add(heroi);
            _context.SaveChanges();
           
            return Ok();
        }
        
        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {
            _context.AddRange(
                new Heroi { Nome = "Capitão América" },
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viuva Negra" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Capitã Marvel" }
                );
            _context.SaveChanges();

            return Ok();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("Delete/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpGet("Delete/{id}")]
        public void Delete(int id)
        {
            var heroi = _context.Herois
                        .Where(x => x.Id == id)
                        .Single();
            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
