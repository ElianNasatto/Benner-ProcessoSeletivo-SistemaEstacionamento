﻿using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EstacionadoRepository : IEstacionadoRepository
    {

        private SistemContext context = new UnitOfWork().ObterContexto();

        public bool Alterar(Estacionado estacionado)
        {
            Estacionado estacionadoOriginal = context.Estacionados.First(x => x.IdEstacionado == estacionado.IdEstacionado);
            if (estacionadoOriginal == null)
            {
                return false;
            }
            else
            {
                estacionadoOriginal.IdEstacionado = estacionado.IdEstacionado;
                estacionadoOriginal.DataSaida = estacionado.DataSaida;
                estacionadoOriginal.DataEntrada = estacionado.DataEntrada;
                estacionadoOriginal.TempoCobrado = estacionado.TempoCobrado;
                estacionadoOriginal.Duracao = estacionado.Duracao;
                estacionadoOriginal.ValorPagar = estacionado.ValorPagar;
                estacionadoOriginal.IdPreco = estacionado.IdPreco;
                estacionado.RegistroAtivo = false;
                context.SaveChanges();
                    
                return true;
            }
        }

        public bool Apagar(int id)
        {
            var estacionadoOrginal = context.Estacionados.Where(x => x.IdEstacionado == id).FirstOrDefault();
            if (estacionadoOrginal == null)
            {
                return false;
            }
            else
            {
                estacionadoOrginal.RegistroAtivo = false;
                int rowsAffected = context.SaveChanges();
                return rowsAffected == 1;
            }

        }

        public bool Inserir(Estacionado estacionado)
        {
            estacionado.RegistroAtivo = true;
            context.Estacionados.Add(estacionado);
            var rowAffected = context.SaveChanges();
            return rowAffected == 1;
        }

        public Estacionado ObterPelaPlaca(string placa)
        {
            return context.Estacionados.Include("carro").Where(x => x.Carro.Placa == placa).FirstOrDefault();
        }

        public Estacionado ObterPeloId(int idEstacionado)
        {
            return context.Estacionados.Include("carro").First(x => x.IdEstacionado == idEstacionado);
        }

        public List<Estacionado> ObterTodosAberto()
        {
            return context.Estacionados.Include("Carro").Where(x => x.RegistroAtivo == true).ToList();
            
        }

        public List<Estacionado> ObterTodosFechados()
        {
            return context.Estacionados.Include("Carro").Include("Preco").Where(x=> x.RegistroAtivo == false).ToList();
        }

        public List<Estacionado> ObterTodosPelaPlaca(string placa)
        {
            return context.Estacionados.Include("carro").Include("preco").Where(x => x.Carro.Placa == placa).ToList();
        }

        public bool VerificaJaEstaEstacionado(string placa)
        {
            var estacionado = context.Estacionados.Include("carro").Where(x => x.Carro.Placa == placa && x.RegistroAtivo == true).FirstOrDefault();
            if (estacionado == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
