using KaasService.Models;
using KaasService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KaasService.Controllers
{
    [Route("kazen")]
    [ApiController]
    public class KaasController : ControllerBase
    {
        private readonly IKaasRepository repo;
        public KaasController(IKaasRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return base.Ok(await repo.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var kaas = await repo.FindByIdAsync(id);
            if (kaas == null)
            {
                return base.NotFound();
            }
            return base.Ok(kaas);
        }
        [HttpGet("smaak")]
        public async Task<IActionResult> FindBySmaak(string smaak)
        {
            return base.Ok(await repo.FindBySmaakAsync(smaak));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Kaas kaas)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    kaas.Id = id;
                    await repo.UpdateAsync(kaas);
                    return base.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return base.NotFound();
                }
                catch { return base.Problem(); }
            }
            return base.BadRequest(this.ModelState);
        }
    }
}
