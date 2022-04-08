using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaasService.Models;
using Microsoft.EntityFrameworkCore;

namespace KaasService.Repositories
{
    public class KaasRepository : IKaasRepository
    {
        private readonly KaaslandContext context;
        public KaasRepository(KaaslandContext context)
        {
            this.context = context;
        }
        public async Task<Kaas> FindByIdAsync(int id)
        {
            return await context.Kazen.FindAsync(id);
        }

        public async Task<List<Kaas>> GetAllAsync()
        {
            return await context.Kazen.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(Kaas kaas)
        {
            context.Kazen.Update(kaas);
            await context.SaveChangesAsync();
        }

        public async Task<List<Kaas>> FindBySmaakAsync(string smaak)
        {
            return await context.Kazen.AsNoTracking().Where(k => k.Smaak == smaak).ToListAsync();
        }
    }
}
