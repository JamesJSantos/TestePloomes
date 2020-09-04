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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using System.Net;
using Microsoft.AspNetCore.Http;

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

        public async Task<ResponseViewModel> Create(ClienteViewModel cliente)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var cpfrepetido = await VerificaCPFExistente(cliente.CPF);

                if (cpfrepetido)
                {
                    response.Status = false;
                    response.ErrorMessage = "CPF Já cadastrado no sistema. Utilize outro CPF.";
                    return response;
                }
                    

                var cpfValido = await ValidarCPF(cliente.CPF);

                if (!cpfValido)
                {
                    response.Status = false;
                    response.ErrorMessage = "O CPF informado é inválido.";
                    return response;
                }

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

                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseViewModel> Edit(ClienteViewModel cliente)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var cpfValido = await ValidarCPF(cliente.CPF);

                if (!cpfValido)
                {
                    response.ErrorMessage = "CPF invalido.";
                    response.Status = false;
                    return response;
                }
                  

                var clienteeditado = await _contexto.Clientes.FirstOrDefaultAsync(c => c.Id == cliente.Id);

                clienteeditado.Nome = cliente.Nome;
                clienteeditado.CPF = cliente.CPF;
                clienteeditado.Idade = cliente.Idade;
                clienteeditado.Sexo = cliente.Sexo;
                clienteeditado.Celular = cliente.Celular;
                clienteeditado.Email = cliente.Email;

                await _contexto.SaveChangesAsync();
                response.Status = true;
                return response;
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

        public async Task<bool> VerificaCPFExistente(string cpf)
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

        public async Task<bool> ValidarCPF(string CPF)
        {
            string valor = CPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
            {
                if (valor[i] != valor[0])
                    igual = false;
            }

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(valor[i].ToString());
            }

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += (11 - i) * numeros[i];
            }

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
            {
                if (numeros[10] != 11 - resultado)
                    return false;
            }

            return true;
        }
    }
}



