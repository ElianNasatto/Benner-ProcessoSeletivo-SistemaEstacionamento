using Model;
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

        private SistemContext context = new SistemContext();
        public bool Alterar(Estacionado estacionado)
        {
            throw new NotImplementedException();
        }

        public bool Apagar(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Estacionado estacionado)
        {
            throw new NotImplementedException();
        }

        public Estacionado ObterPelaPlaca(int placa)
        {
            throw new NotImplementedException();
        }

        public List<Estacionado> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
