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

        public int Inserir(Estacionado estacionado)
        {
            estacionado.RegistroAtivo = true;
            context.Estacionados.Add(estacionado);
            context.SaveChanges();
            return estacionado.IdEstacionado;
        }

        public Estacionado ObterPelaPlaca(string placa)
        {
            return context.Estacionados.Where(x => x.Carro.Placa == placa).FirstOrDefault();
        }

        public List<Estacionado> ObterTodos()
        {
            return context.Estacionados.ToList();
        }
    }
}
