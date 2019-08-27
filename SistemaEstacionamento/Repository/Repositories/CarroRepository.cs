using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        private SistemContext context = new SistemContext();

        public bool Alterar(Carro carro)
        {
            carro.RegistroAtivo = true;
            var carroAntigo = context.Carros.FirstOrDefault(x => x.Id == carro.Id);
            if (carroAntigo == null)
            {
                return false;
            }
            else
            {
                carroAntigo.Placa = carro.Placa;
                int rowAffected = context.SaveChanges();
                return rowAffected == 1;
            }
        }

        public bool Apagar(int id)
        {
            // Caso o first nao encontre o registro ele retorna um exxeption ja o default ele retorna null
            var carro = context.Carros.FirstOrDefault(x => x.Id == id);
            if (carro == null)
            {
                return false;
            }
            else
            {
                carro.RegistroAtivo = false;
                context.SaveChanges();
                return true;
            }
        }

        public int Inserir(Carro carro)
        {
            carro.RegistroAtivo = true;
            context.Carros.Add(carro);
            context.SaveChanges();
            return carro.Id;
        }

        public Carro ObterPelaPlaca(string placa)
        {
            Carro carro = context.Carros.FirstOrDefault(x => x.Placa == placa && x.RegistroAtivo == true);
            return carro;
        }

        public List<Carro> ObterTodos()
        {
            return context.Carros.Where(x => x.RegistroAtivo == true).ToList();
        }
    }
}
