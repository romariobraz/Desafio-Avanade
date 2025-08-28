using Avanade.Contexto;
using Entity.BD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avanade.Controllers
{
    [ApiController]
    [Authorize] //para todos os métodos deste controller
    [Route("[Controller]")]
    public class EstoqueController : ControllerBase
    {
        private readonly ProdutoContexto _Contexto;
        public EstoqueController(ProdutoContexto contexto)
        {
            _Contexto = contexto;
        }
        #region Cadastro
        //cadastrar produto
        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            if (produto == null || string.IsNullOrEmpty(produto.Nome_Produto) || produto.Preco <= 0 || produto.Quantidade < 0)
            {
                return BadRequest("Dados do produto inválidos.");
            }

            _Contexto.Produtos.Add(produto);
            _Contexto.SaveChanges();
            return CreatedAtAction(nameof(ObterProdutoPorId), new { id = produto.Id_Produto }, produto);
        }
        #endregion

        #region Atualizar
        //atualizar produto
        [HttpPut("{id}")]
        public IActionResult AtualizarProduto(int id, [FromBody] Produto produto)
        {
            var produtoExistente = _Contexto.Produtos.Find(id);
            if (produtoExistente == null)
            {
                return NotFound("Produto não encontrado.");
            }
            if (produto == null || string.IsNullOrEmpty(produto.Nome_Produto) || produto.Preco <= 0 || produto.Quantidade < 0)
            {
                return BadRequest("Dados do produto inválidos.");
            }
            produtoExistente.Nome_Produto = produto.Nome_Produto;
            produtoExistente.Descricao_Produto = produto.Descricao_Produto;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Quantidade = produto.Quantidade;
            produtoExistente.Ativo = produto.Ativo;
            _Contexto.SaveChanges();
            return NoContent();
        }
        #endregion

        #region Obter por Id
        //Obter um produto por id
        [HttpGet("{id}")]
        public IActionResult ObterProdutoPorId(int id)
        {
            var produto = _Contexto.Produtos.Find(id);
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }
            return Ok(produto);
        }
        #endregion
    }
}
