using RabbitMQProduct.WebApiApp.Models;

namespace RabbitMQProduct.WebApiApp.RabbitMQ
{
    public interface IRabbitMQProducer
    {
       public void SendProductMessage<T>(T message);
       //public void SendProductMessage<Product>(Product productData);
    }
}