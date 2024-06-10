﻿using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
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

        public async Task<bool> Borrar(int Id)
        {
            var elementoABorrar = await DameUno(Id);
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

        public async Task<List<T>> DameTodos()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> DameUno(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> Filtra(Expression<Func<T, bool>> predicado)
        {
            return await _context.Set<T>().Where(predicado).ToListAsync();
        }

        public async Task<int> Modificar(int Id, T element)
        {
            _context.Set<T>().Update(element);
            return await _context.SaveChangesAsync();
        }
    }
}
