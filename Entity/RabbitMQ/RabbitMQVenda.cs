using Entity.BD;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMQProducer
{
    private readonly string _hostName = "localhost";

    public void EnviarPedidoConfirmado(Pedido pedido)
    {
        var factory = new ConnectionFactory() { HostName = _hostName };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "vendas", type: ExchangeType.Fanout);

        var mensagem = JsonSerializer.Serialize(pedido);
        var body = Encoding.UTF8.GetBytes(mensagem);

        channel.BasicPublish(exchange: "vendas",
                             routingKey: "",
                             basicProperties: null,
                             body: body);

        Console.WriteLine($"Pedido {pedido.Id_Pedido} enviado para o RabbitMQ.");
    }
}