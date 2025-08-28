using Avanade.Contexto;
using Entity.BD;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class RabbitMQConsumer
{
    private readonly ProdutoContexto _contexto;
    private readonly string _hostName = "localhost";

    public RabbitMQConsumer(ProdutoContexto contexto)
    {
        _contexto = contexto;
    }

    public void EscutarPedidos()
    {
        var factory = new ConnectionFactory() { HostName = _hostName };
        using var connectionTask = factory.CreateConnection();
        var channel = connectionTask.CreateModel();

        channel.ExchangeDeclare(exchange: "vendas", type: ExchangeType.Fanout);
        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName, exchange: "vendas", routingKey: "");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var mensagem = Encoding.UTF8.GetString(body);
            var pedido = JsonSerializer.Deserialize<PedidoItem>(mensagem);

            if (pedido != null)
            {
                foreach (var item in new List<PedidoItem> { pedido })
                {
                    var produto = _contexto.Produtos.Find(item.Id_Produto);
                    if (produto != null)
                        produto.Quantidade -= item.Quantidade;
                }
                _contexto.SaveChanges();
                Console.WriteLine($"Estoque atualizado para o pedido {pedido.Id_Pedido}");
            }
            await Task.CompletedTask;
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        Console.WriteLine("Consumidor RabbitMQ iniciado. Pressione CTRL+C para sair.");
        while (true) { } // mantém o serviço rodando
    }
}
