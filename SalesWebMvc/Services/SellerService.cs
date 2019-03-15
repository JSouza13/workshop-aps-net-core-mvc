using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FinAllAsync()
        {
            // Retorna lista de vendedores cadastrados no DB
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            // Através do entity está sendo feito um join entre Departamento e Vendedores, conhecido como eager load
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            // Verifica se não existe o Id no DB
            if (!hasAny)
            {
                // Se o Id não existir, retorna mensagem
                throw new NotFoundException("Id not found");
            }
            try
            {
                // Se existir o Id, tenta fazer update e salvar no DB
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                // Se não conseguir fazer update, retorna mensagem
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
