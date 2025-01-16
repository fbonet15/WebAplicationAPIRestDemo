using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPIDemo.DAL.Service;
using WebApplicationAPIDemo.Model;


namespace WebApplicationAPIDemo.Controllers
{
    [EnableCors]
    [Route("api/Responsables")]
    [ApiController]
    public class ResponsablesController : ControllerBase
    {
        // GET: users
        [HttpGet]
        public List<Responsable> Get()
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.GetAll();
        }

        // GET users/5
        [HttpGet("{id}")]
        public Responsable Get(long id)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.GetById(id);
        }

        // POST users
        [HttpPost]
        public Responsable Post([FromBody] Responsable responsable)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.Add(responsable);
        }

        // PUT users/5
        [HttpPut("{id}")]
        public long Put(long id, [FromBody] Responsable responsable)
        {
            ResponsableService objResponsableService = new ResponsableService();
            return objResponsableService.Update(responsable);
        }

        // DELETE users/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ResponsableService objResponsableService = new ResponsableService();
            objResponsableService.Delete(id);
        }
    }
}
