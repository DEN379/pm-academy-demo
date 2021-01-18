﻿using School.Services;
using System;
using System.Threading.Tasks;

namespace School
{
    public class Message : BaseEntity<Guid>
    {
        private Message(Guid id, DateTime createdAt, string type, string payload) 
        {
            this.Id = id;
            this.CreatedAt = createdAt;
            this.Type = type;
            this.Payload = payload;
        }

        public DateTime CreatedAt { get; }
        public DateTime? ProcessedAt { get; private set; }
        public string Type { get; }
        public string Payload { get; }

        public async Task Process(IMessagePublisher publisher, System.Threading.CancellationToken cancellationToken)
        {
            await publisher.PublishAsync(this, cancellationToken);
            this.ProcessedAt = DateTime.UtcNow;
        }

        public static Message FromDomainEvent<TE>(TE @event, IEventSerializer serializer) where TE : IDomainEvent
        {
            if (null == @event)
                throw new ArgumentNullException(nameof(@event));
            if (null == serializer)
                throw new ArgumentNullException(nameof(serializer));

            var type = @event.GetType().FullName;

            return new Message(Guid.NewGuid(), DateTime.UtcNow, type, serializer.Serialize(@event));
        }

    }
}