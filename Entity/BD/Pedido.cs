using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.BD
{
    public class Pedido
    {
        [Key]
        public int Id_Pedido { get; set; }
        [Required]
        public string Cliente { get; set; }
        [Required]
        public string Telefone_Contato { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public DateTime Data_Pedido { get; set; }
        public decimal Valor_Total { get; set; }
        public List<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
    }
}
