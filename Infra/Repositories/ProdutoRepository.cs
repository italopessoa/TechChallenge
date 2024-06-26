﻿using Domain.Entities;
using Domain.Repositories;
using Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Produto produto)
        {
            try
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar cliente. {ex}");
            }
        }

        public async Task<Produto> GetById(int Id)
        {
            try
            {
                return _context.Produtos
                    .Include(x => x.CategoriaProduto)
                    .FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar produto. {ex}");
            }
        }

        public async Task<IList<Produto>> GetAll()
        {
            try
            {
                return _context.Produtos
                    .Include(x => x.CategoriaProduto).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar produtos. {ex}");
            }
        }

        public async Task<IList<Produto>> GetByCategoria(int IdCategoria)
        {
            try
            {
                return _context.Produtos
                    .Include(x => x.CategoriaProduto).Where(x => x.CategoriaProduto.Id == IdCategoria).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar produtos. {ex}");
            }
        }

        public async Task<Produto> Post(Produto produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar cliente. {ex}");
            }
        }

        public async Task<Produto> Update(Produto produto)
        {
            try
            {
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar cliente. {ex}");
            }
        }
    }
}
