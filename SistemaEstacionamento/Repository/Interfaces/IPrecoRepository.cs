using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPrecoRepository
    {
        int Inserir(Preco preco);

        bool Alterar(Preco preco);

        List<Preco> ObterTodos();

        bool Apagar(int id);

        Preco ObterPeloId(int id);

        bool VerificaJaCadastrado(DateTime dataInicial, DateTime dataFinal);
    }
}
