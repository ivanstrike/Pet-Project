using System.Threading.Channels;
using PetProject.Application.MessageQueues;

namespace PetProject.Infrastructure.MessageQueues;

public class InMemoryMessageQueue<TMessage>: IMessageQueue<TMessage>
{
    private readonly Channel<TMessage> _channel;
    
    public InMemoryMessageQueue(Channel<TMessage> channel)
    {
        _channel = channel;
    }
    public async Task WriteAsync(TMessage paths, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(paths, cancellationToken);
    }
    public async Task<TMessage> ReadAsync(CancellationToken cancellationToken = default)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}