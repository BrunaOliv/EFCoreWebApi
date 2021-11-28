﻿using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repo
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly HeroiContext _context;

        public EFCoreRepository(HeroiContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
            //_context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return(await _context.SaveChangesAsync()) > 0;
        }

        public async Task<IEnumerable<Heroi>> GetAllHerois(bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                .ThenInclude(heroib => heroib.Batalha);

            }

            query = query.AsNoTracking().OrderBy(h => h.Id);
            return await query.ToListAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                .ThenInclude(heroib => heroib.Batalha);

            }

            query = query.AsNoTracking().OrderBy(h => h.Id);
            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Heroi>> GetHeroisNome(string nome, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                .ThenInclude(heroib => heroib.Batalha);

            }

            query = query.AsNoTracking()
                .Where(h => h.Nome.Contains(nome))
                .OrderBy(h => h.Id);
            return await query.ToArrayAsync();
        }
    }
}
