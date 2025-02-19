﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEstacionadoRepository
    {
        bool Inserir(Estacionado estacionado);

        bool Alterar(Estacionado estacionado);

        List<Estacionado> ObterTodosAberto();

        List<Estacionado> ObterTodosFechados();

        Estacionado ObterPelaPlaca(string placa);

        Estacionado ObterPeloId(int idEstacionado);

        bool VerificaJaEstaEstacionado(string placa);

        List<Estacionado> ObterTodosPelaPlaca(string placa);
    }
}
