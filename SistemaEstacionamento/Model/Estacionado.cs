using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("estacionado")]
    public class Estacionado
    {
        [Key,Column("id_estacionado")]
        public int IdEstacionado { get; set; }

        [Column("id_carro")]
        public int IdCarro { get; set; }

        [ForeignKey("IdCarro")]
        public Carro Carro { get; set; }
        
        [Column("id_preco")]
        public int? IdPreco { get; set; }

        [ForeignKey("IdPreco")]
        public Preco Preco { get; set; }

        [Column("data_entrada")]
        public DateTime DataEntrada { get; set; }

        [Column("data_saida")]
        public DateTime DataSaida { get; set; }

        [Column("duracao")]
        public decimal Duracao { get; set; }

        [Column("tempo_cobrado")]
        public int TempoCobrado { get; set; }

        [Column("valor_pagar")]
        public decimal ValorPagar { get; set; }

        [Column("registro_ativo")]
        public bool RegistroAtivo { get; set; }
    }
}
