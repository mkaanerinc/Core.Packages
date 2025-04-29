using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using Core.Infrastructure.Messaging.RabbitMQ.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Services;

public class RabbitMQConnectionFactory : IRabbitMQConnectionFactory
{
    private readonly ConnectionFactory _connectionFactory;

    public RabbitMQConnectionFactory(RabbitMQSettings settings)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.Port,
            UserName = settings.UserName,
            Password = settings.Password,
            AutomaticRecoveryEnabled = true
        };
    }

    public Task<IConnection> CreateConnectionAsync()
    {
        return _connectionFactory.CreateConnectionAsync();
    }
}