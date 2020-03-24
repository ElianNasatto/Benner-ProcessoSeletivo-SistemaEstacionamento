using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork
    {
        public static SistemContext SistemContext;

        public SistemContext ObterContexto()
        {
            if (SistemContext == null)
            {
                SistemContext = new SistemContext();
            }

            return SistemContext;
        }


    }
}
