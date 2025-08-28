using Avanade.Contexto;
using Entity.BD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avanade.Controllers

{
    [ApiController]
    [Authorize]
    [Route("[Controller]")]
    public class VendasController : ControllerBase
    {
        private readonly VendasContexto _Contexto;
        public VendasController(VendasContexto contexto)
        {
            _Contexto = contexto;
        }

        #region Criar Pedido com movimentações no estoque
        //Criar Pedido
        [HttpPost]
        public IActionResult CriarPedido([FromBody]Pedido pedido)
        {
            //Primeiro vamos validar as quantidades que não podem ser negativas ou zero e o produto deve existir
            if (pedido == null || pedido.Itens == null || !pedido.Itens.Any())
            {
                return BadRequest("Pedido inválido. Nenhum item informado.");
            }
            //Agora vamos validar a quantidade de produtos no banco
            decimal valorTotal = 0;
            foreach (var item in pedido.Itens)
            {
                var produto = _Contexto.Produtos.FirstOrDefault(p => p.Id_Produto == item.Id_Produto);

                if (produto == null)
                {
                    return NotFound($"Produto com ID {item.Id_Produto} não encontrado.");
                }

                if (produto.Quantidade < item.Quantidade)
                {
                    return BadRequest($"Estoque insuficiente para o produto {produto.Nome_Produto}. Estoque atual: {produto.Quantidade}");
                }

                // Atualiza estoque
                produto.Quantidade -= item.Quantidade;

                // Atualiza valor total
                item.PrecoUnitario = produto.Preco;
                valorTotal += item.Quantidade * produto.Preco;
            }

            pedido.Data_Pedido = DateTime.Now;
            pedido.Valor_Total = valorTotal;

            //Ajusta data para UTC
            pedido.Data_Pedido = DateTime.SpecifyKind(pedido.Data_Pedido, DateTimeKind.Utc);

            _Contexto.Pedidos.Add(pedido);
            _Contexto.SaveChanges();

            // Envia evento para RabbitMQ
            var producer = new RabbitMQProducer();
            var evento = new Pedido
            {
                Id_Pedido = pedido.Id_Pedido,
                Itens = pedido.Itens.Select(i => new PedidoItem
                {
                    Id_Produto = i.Id_Produto,
                    Quantidade = i.Quantidade
                }).ToList()
            };
            producer.EnviarPedidoConfirmado(evento);

            return Ok(pedido);
        }
        #endregion

        #region Consultar Pedido
        //Consultar Pedido
        [HttpGet("{id}")]
        public IActionResult ConsultarPedido(int id)
        {
            var pedido = _Contexto.Pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound("Pedido não encontrado.");
            }
            return Ok(pedido);
        }
        #endregion
    }
}
