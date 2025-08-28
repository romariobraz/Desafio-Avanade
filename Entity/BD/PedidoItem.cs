using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.BD
{
    public class PedidoItem
    {
        [Key]
        public int Id { get; set; }

        public int Id_Pedido { get; set; }

        public int Id_Produto { get; set; }

        public int Quantidade { get; set; }
        
        public decimal PrecoUnitario { get; set; }
        public decimal ValorTotal => Quantidade * PrecoUnitario;
    }
}
