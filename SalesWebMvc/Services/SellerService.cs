﻿using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FinAll()
        {
            // Retorna lista de vendedores cadastrados no DB
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            // Através do entity está sendo feito um join entre Departamento e Vendedores, conhecido como eager load
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            // Verifica se não existe o Id no DB
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                // Se o Id não existir, retorna mensagem
                throw new NotFoundException("Id not found");
            }
            try
            {
                // Se existir o Id, tenta fazer update e salvar no DB
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                // Se não conseguir fazer update, retorna mensagem
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
