using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ArticlesManager.RabbitMqController
{
    public class RabbitManager
    {
        private const string HostName = "localhost";
        private const string VirtualHost = "";
        private const bool IsDurable = false;

        public RabbitManager()
        {
            try
            {
                SetTheRabbit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        internal void Listen()
        {
            var consumer = new EventingBasicConsumer(Channel);
            Channel.BasicConsume(
                "api",
                true,
                consumer);
            consumer.Received += async (model, req) =>
            {
                var body = Encoding.UTF8.GetString(req.Body);
             
                using (var file = File.CreateText($"jsons/crawler/{Environment.GetEnvironmentVariable("crawlerFileId")}.json"))
                {
                    file.Write("");
                    await file.WriteAsync(body);
                    Environment.SetEnvironmentVariable("crawlerFileId",int.Parse(Environment.GetEnvironmentVariable("crawlerFileId")).ToString());
                }
            };
        }
        
        public void Send(string value)
        {
            var props = Channel.CreateBasicProperties();
            props.DeliveryMode = 2;
            Channel.BasicPublish(
                "",
                "crawler",
                props,
                Encoding.UTF8.GetBytes(value)
            );
            Console.WriteLine("Message sent");
        }
        
        private void SetTheRabbit()
        {
            ConnectionFactory = new ConnectionFactory
            {
                HostName = HostName
            };
            if (!string.IsNullOrEmpty(VirtualHost)) ConnectionFactory.VirtualHost = VirtualHost;
            Connection = ConnectionFactory.CreateConnection();
            Channel = Connection.CreateModel();
            var args = new Dictionary<string, object> {{"max-length-bytes", 1073741824}};
            Channel.QueueDeclare(
                queue:"api",
                true,
                false,
                false,
                 args
            );
            
        }
        private ConnectionFactory ConnectionFactory { get; set; }

        private IConnection Connection { get; set; }

        private IModel Channel { get; set; }
    }
}