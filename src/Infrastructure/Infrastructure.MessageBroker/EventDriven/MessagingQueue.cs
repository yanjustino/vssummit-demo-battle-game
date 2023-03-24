using System.Collections.Concurrent;

namespace Infrastructure.MessageBroker.EventDriven;

public class MessagingQueue
{
    public ConcurrentDictionary<string, Queue<object>> Queue { get; }

    public MessagingQueue()
    {
        Queue = new ConcurrentDictionary<string, Queue<object>>();
    }

    public async Task<object?> Pop(string key)
    {
        await Task.Yield();
        if (!Queue.ContainsKey(key)) return null;
        
        Queue[key].TryDequeue(out var message);
        return message;
    }

    public async Task Push(string key, object data)
    {
        await Task.Yield();
        
        if (!Queue.ContainsKey(key)) 
            Queue[key] = new Queue<object>();
        
        Queue[key].Enqueue(data);
    }
}