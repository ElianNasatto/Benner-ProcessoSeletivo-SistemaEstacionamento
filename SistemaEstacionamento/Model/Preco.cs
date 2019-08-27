using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("precos")]
    public class Preco
    {
        [Key,Column("id_preco")]
        public int IdPreco { get; set; }

        [Column("preco_hora")]
        public decimal PrecoHora { get; set; }

        [Column("data_inicial")]
        public DateTime DataInicial { get; set; }

        [Column("data_final")]
        public DateTime DataFinal { get; set; }

        [Column("registro_ativo")]
        public bool RegistroAtivo { get; set; }
    }
}
