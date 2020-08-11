using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TestePloomes.Context;
using TestePloomes.Models;
using TestePloomes.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace TestePloomes.Services
{
    public class ClienteService
    {
        private readonly Contexto _contexto;
        public ClienteService(Contexto contexto)
        {
            _contexto = contexto;
        }


        public async Task<List<ClienteViewModel>> GetAll()
        {
            try
            {
                var clientes = await _contexto.Clientes.Select(c => new ClienteViewModel
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Idade = c.Idade,
                    Sexo = c.Sexo,
                    Celular = c.Celular,
                    Email = c.Email
                }).ToListAsync();

                if (clientes == null)
                    return null;

                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ClienteViewModel> GetById(int id)
        {
            ClienteViewModel clienteEncontrado = new ClienteViewModel();
            try
            {
                var busca = await _contexto.Clientes.FirstOrDefaultAsync(c => c.Id == id);

                if (busca == null)
                    return null;

                clienteEncontrado = new ClienteViewModel
                {
                    Id = busca.Id,
                    Nome = busca.Nome,
                    CPF = busca.CPF,
                    Idade = busca.Idade,
                    Sexo = busca.Sexo,
                    Celular = busca.Celular,
                    Email = busca.Email
                };

                return clienteEncontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Create(ClienteViewModel cliente)
        {
            try
            {
                var cpfrepetido = await VerificaCPFExistente(cliente.CPF);

                if (cpfrepetido)
                    return false;

                var clientenovo = new Cliente
                {
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    Idade = cliente.Idade,
                    Sexo = cliente.Sexo,
                    Celular = cliente.Celular,
                    Email = cliente.Email
                };

                await _contexto.AddAsync(clientenovo);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Edit(ClienteViewModel cliente)
        {
            try
            {
                var cpfrepetido = await VerificaCPFExistente(cliente.CPF);

                if (cpfrepetido)
                    return false;

                var clienteeditado = await _contexto.Clientes.FirstOrDefaultAsync(c => c.Id == cliente.Id);

                clienteeditado.Nome = cliente.Nome;
                clienteeditado.CPF = cliente.CPF;
                clienteeditado.Idade = cliente.Idade;
                clienteeditado.Sexo = cliente.Sexo;
                clienteeditado.Celular = cliente.Celular;
                clienteeditado.Email = cliente.Email;

                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var clienteexcluido = await _contexto.Clientes.FirstOrDefaultAsync(c => c.Id == id);
                if (clienteexcluido == null)
                    return false;

                _contexto.Clientes.Remove(clienteexcluido);
                await _contexto.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> VerificaCPFExistente (string cpf)
        {
            try
            {
                var cpfrepetido = await _contexto.Clientes.AnyAsync(c => c.CPF == cpf);
                return cpfrepetido;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



