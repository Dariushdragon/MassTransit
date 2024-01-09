namespace MassTransit.SqlTransport
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MassTransit.Middleware;
    using Topology;


    public class ScopeClientContext :
        ScopePipeContext,
        ClientContext
    {
        readonly ClientContext _context;

        public ScopeClientContext(ClientContext context, CancellationToken cancellationToken)
            : base(context)
        {
            _context = context;
            CancellationToken = cancellationToken;
        }

        public override CancellationToken CancellationToken { get; }

        public ConnectionContext ConnectionContext => _context.ConnectionContext;

        public Task<long> CreateQueue(Queue queue)
        {
            return _context.CreateQueue(queue);
        }

        public Task<long> CreateTopic(Topic topic)
        {
            return _context.CreateTopic(topic);
        }

        public Task<long> CreateTopicSubscription(TopicToTopicSubscription subscription)
        {
            return _context.CreateTopicSubscription(subscription);
        }

        public Task<long> CreateQueueSubscription(TopicToQueueSubscription subscription)
        {
            return _context.CreateQueueSubscription(subscription);
        }

        public Task<long> PurgeQueue(string queueName, CancellationToken cancellationToken)
        {
            return _context.PurgeQueue(queueName, cancellationToken);
        }

        public Task Send<T>(string queueName, SqlMessageSendContext<T> context)
            where T : class
        {
            return _context.Send(queueName, context);
        }

        public Task<IEnumerable<MessageDelivery>> Publish<T>(string topicName, SqlMessageSendContext<T> context)
            where T : class
        {
            return _context.Publish(topicName, context);
        }

        public Task<bool> RenewLock(Guid lockId, long messageDeliveryId, TimeSpan duration)
        {
            return _context.RenewLock(lockId, messageDeliveryId, duration);
        }

        public Task<bool> Unlock(Guid lockId, long messageDeliveryId, TimeSpan delay, SendHeaders sendHeaders)
        {
            return _context.Unlock(lockId, messageDeliveryId, delay, sendHeaders);
        }

        public Task<IEnumerable<SqlTransportMessage>> ReceiveMessages(string queueName, SqlReceiveMode mode, int messageLimit, TimeSpan lockDuration)
        {
            return _context.ReceiveMessages(queueName, mode, messageLimit, lockDuration);
        }

        public Task<bool> DeleteMessage(Guid lockId, long messageDeliveryId)
        {
            return _context.DeleteMessage(lockId, messageDeliveryId);
        }

        public Task<bool> DeleteScheduledMessage(Guid tokenId)
        {
            return _context.DeleteScheduledMessage(tokenId);
        }

        public Task<bool> MoveMessage(Guid lockId, long messageDeliveryId, string queueName, SqlQueueType queueType, SendHeaders sendHeaders)
        {
            return _context.MoveMessage(lockId, messageDeliveryId, queueName, queueType, sendHeaders);
        }
    }
}
