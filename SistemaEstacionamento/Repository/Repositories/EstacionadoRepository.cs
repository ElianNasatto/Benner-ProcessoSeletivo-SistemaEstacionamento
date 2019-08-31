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

        public List<Estacionado> ObterTodos()
        {
            return context.Estacionados.Include("carro").Where(x => x.RegistroAtivo == true).ToList();
        }
    }
}
