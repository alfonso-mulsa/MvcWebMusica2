
﻿using MvcWebMusica2.Models;

using System.Linq.Expressions;

namespace MvcWebMusica2.Services.Repositorio
{
    public class EFGenericRepositorio<T> : IGenericRepositorio<T> where T : class
    {
        private readonly GrupoBContext _context = new();
        public async Task<bool> Agregar(T element)
        {
            await _context.Set<T>().AddAsync(element);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Borrar(int id)
        {
            var elementoABorrar = await DameUno(id);
            if (elementoABorrar != null)
            {
                _context.Set<T>().Remove(elementoABorrar);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        // Aconsejado por ReSharper
        //public Task<List<T>> DameTodos()
        //{
        //    return Task.FromResult(_context.Set<T>().AsParallel().ToList());
        //    //return await _context.Set<T>().AsNoTracking().ToListAsync();
        //}

        public Task<List<T>> DameTodos()
        {
            return Task.FromResult(_context.Set<T>().AsParallel().ToList());
            //return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        //    Metodo que permite especificar INCLUDE.
        //public async Task<List<T>> DameTodos(params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = _context.Set<T>();
        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }
        //    return await query.ToListAsync();
        //}


        public async Task<T?> DameUno(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _context.Set<T>().FindAsync(id);
        }

        public Task<List<T>> Filtra(Expression<Func<T, bool>> predicado)
        {
            return Task.FromResult(_context.Set<T>().Where(predicado).AsParallel().ToList());
            //return _context.Set<T>().Where<T>(predicado).AsParallel().ToList();
            //return await _context.Set<T>().Where<T>(predicado).ToListAsync();
        }

        public async Task<int> Modificar(int id, T element)
        {
            _context.Set<T>().Update(element);
            return await _context.SaveChangesAsync();
        }
    }
}
