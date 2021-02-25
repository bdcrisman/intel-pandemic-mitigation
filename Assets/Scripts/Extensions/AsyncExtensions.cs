using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public static class AsyncExtensions
{
    /// <summary>
    /// Version of whenall that can be canceled.
    /// https://www.reddit.com/r/csharp/comments/an2vgd/cancelling_taskwhenall_after_a_certain_amount_of/
    /// </summary>
    /// <param name="tasks"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task WhenAll(this IEnumerable<Task> tasks, CancellationToken cancellationToken)
    {
        if (tasks == null)
            throw new ArgumentNullException(nameof(tasks), $"{nameof(tasks)} is null.");

        await Task.WhenAny(Task.WhenAll(tasks), cancellationToken.AsTask()).ConfigureAwait(false);
        cancellationToken.ThrowIfCancellationRequested();
    }

    public static async Task WhenAll(this List<Task> tasks, CancellationToken cancellationToken)
    {
        if (tasks == null)
            throw new ArgumentNullException(nameof(tasks), $"{nameof(tasks)} is null.");

        await Task.WhenAny(Task.WhenAll(tasks), cancellationToken.AsTask()).ConfigureAwait(false);
        cancellationToken.ThrowIfCancellationRequested();
    }

    /// <summary>
    /// CancellationToken as a task.
    /// https://stackoverflow.com/questions/18670111/task-from-cancellation-token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task AsTask(this CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<object>();
        cancellationToken.Register(() => tcs.TrySetCanceled(),
            useSynchronizationContext: false);
        return tcs.Task;
    }
}
