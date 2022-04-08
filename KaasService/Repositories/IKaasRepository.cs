using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaasService.Models
{
    public interface IKaasRepository
    {
        Task<List<Kaas>> GetAllAsync();
        Task<Kaas> FindByIdAsync(int id);
        Task<List<Kaas>> FindBySmaakAsync(string smaak);
        Task UpdateAsync(Kaas kaas);
    }
}
