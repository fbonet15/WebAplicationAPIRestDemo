using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAplicationAPIRestDemo.DAL.Model;
using WebAplicationAPIRestDemo.DAL.Service;
using WebApplicationAPIDemo.DAL.Service;
using WebApplicationAPIDemo.Model;


namespace WebApplicationAPIDemo.Controllers
{
    [EnableCors]
    [Route("api/itemKanBan")]
    [ApiController]
    public class ItemKanBanController : ControllerBase
    {
        // GET: users
        [HttpGet]
        public List<ItemKanBan> Get()
        {
            ItemKanBanService objItemKanBanService = new ItemKanBanService();
            return objItemKanBanService.GetAll();
        }

        // POST users
        [HttpPost]
        public ItemKanBan Post([FromBody] ItemKanBan itemKanBan)
        {
            ItemKanBanService objItemKanBanService = new ItemKanBanService();
            return objItemKanBanService.Add(itemKanBan);
        }

        // PUT users/5
        [HttpPut("{id}")]
        public long Put(long id, [FromBody] ItemKanBan itemKanBan)
        {
            ItemKanBanService objItemKanBanService = new ItemKanBanService();
            return objItemKanBanService.Update(itemKanBan);
        }

        // DELETE users/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            ItemKanBanService objItemKanBanService = new ItemKanBanService();
            objItemKanBanService.Delete(id);
        }
    }
}