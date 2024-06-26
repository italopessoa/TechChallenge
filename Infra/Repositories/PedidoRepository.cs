﻿using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Repositories;
using Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Pedido>> GetAll()
        {
            try
            {
                return _context.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.FormaPagamento)
                    .Include(x => x.Produtos)
                        .ThenInclude(p => p.CategoriaProduto)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos. {ex}");
            }
        }

        public async Task<Pedido> GetById(int Id)
        {
            try
            {
                return _context.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.FormaPagamento)
                    .Include(x => x.Produtos)
                        .ThenInclude(p => p.CategoriaProduto)
                    .FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedido {Id}. {ex}");
            }
        }

        public async Task<IList<Pedido>> GetByStatus(StatusPedidoEnum status)
        {
            try
            {
                return _context.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.FormaPagamento)
                    .Include(x => x.Produtos)
                        .ThenInclude(p => p.CategoriaProduto)
                    .Where(x => x.StatusPedido == (int)status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos. {ex}");
            }
        }

        public async Task<int> Post(Pedido pedido)
        {
            try
            {
                pedido.IdGuid = Guid.NewGuid();
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                return _context.Pedidos.First(x => x.IdGuid == pedido.IdGuid).Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar pedido. {ex}");
            }
        }

        public async Task Update(Pedido pedido, int Id)
        {
            try
            {
                _context.Pedidos.Update(pedido);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar pedido. {ex}");
            }
        }
    }
}
