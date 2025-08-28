using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.BD
{
    public class Produto
    {
        [Key]
        public int Id_Produto { get; set; }
        public string Nome_Produto { get; set; }
        public string Descricao_Produto { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
        public DateTime Data_Criacao { get; set; }
    }
}
