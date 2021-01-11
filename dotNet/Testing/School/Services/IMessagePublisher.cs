﻿using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace School.Services
{
    public interface IMessagePublisher
    {
        Task PublishAsync(Message message, System.Threading.CancellationToken cancellationToken);
    }

    public class FakeMessagePublisher : IMessagePublisher
    {
        private readonly ILogger<FakeMessagePublisher> _logger;

        public FakeMessagePublisher(ILogger<FakeMessagePublisher> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task PublishAsync(Message message, System.Threading.CancellationToken cancellationToken)
        {
            _logger.LogInformation($"processing message {message.Id}...");
            _logger.LogInformation($"message {message.Id} processed!");
            return Task.CompletedTask;
        }
    }
}