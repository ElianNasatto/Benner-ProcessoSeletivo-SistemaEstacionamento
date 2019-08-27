using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICarroRepository
    {
        int Inserir(Carro carro);

        bool Alterar(Carro carro);

        List<Carro> ObterTodos();

        bool Apagar(int placa);

        Carro ObterPelaPlaca(string placa);
    }
}
