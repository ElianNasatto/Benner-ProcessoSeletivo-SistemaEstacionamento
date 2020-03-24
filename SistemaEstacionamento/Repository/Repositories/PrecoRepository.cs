using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PrecoRepository : IPrecoRepository
    {
        private SistemContext context = new UnitOfWork().ObterContexto();
        public bool Alterar(Preco preco)
        {
            var precoAntigo = context.Precos.Where(x => x.IdPreco == preco.IdPreco && x.RegistroAtivo == true).FirstOrDefault();
            if (precoAntigo == null)
            {
                return false;
            }
            else
            {
                precoAntigo.DataInicial = preco.DataInicial;
                precoAntigo.DataFinal = preco.DataFinal;
                precoAntigo.PrecoHora = preco.PrecoHora;
               int rowAffected = context.SaveChanges();
                return rowAffected == 1;
            }
        }

        public bool Apagar(int id)
        {
            var precoAntigo = context.Precos.Where(x => x.IdPreco == id && x.RegistroAtivo == true).FirstOrDefault();

            if (precoAntigo == null)
            {
                return false; 
            }
            else
            {
                precoAntigo.RegistroAtivo = false;
                context.SaveChanges();
                return true;
            }
        }

        public int Inserir(Preco preco)
        {
            preco.RegistroAtivo = true;
            context.Precos.Add(preco);
            int rowsAffected = context.SaveChanges();
            return rowsAffected;

        }

        public Preco ObterPeloId(int id)
        {
            return context.Precos.FirstOrDefault(x => x.IdPreco == id && x.RegistroAtivo == true);

        }

        public List<Preco> ObterTodos()
        {
            return context.Precos.Where(x => x.RegistroAtivo == true && x.DataFinal > DateTime.Now).ToList();
        }

        public bool VerificaJaCadastrado(DateTime dataInicial, DateTime dataFinal)
        {
            var cadastrado = context.Precos.Where(x => x.DataInicial == dataInicial || x.DataFinal == dataFinal && x.RegistroAtivo == true).FirstOrDefault();
            if (cadastrado == null)
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
