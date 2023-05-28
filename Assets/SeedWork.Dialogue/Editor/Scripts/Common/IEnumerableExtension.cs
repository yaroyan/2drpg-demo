using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public static class IEnumerableExtension
{
    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> values, int chunkSize)
    {
        while (values.Any())
        {
            yield return values.Take(chunkSize).ToList();
            values = values.Skip(chunkSize).ToList();
        }
    }

    public static UniTask WhenAll(this IEnumerable<UniTask> tasks)
    {
        return UniTask.WhenAll(tasks);
    }

    public static UniTask<T[]> WhenAll<T>(this IEnumerable<UniTask<T>> tasks)
    {
        return UniTask.WhenAll(tasks);
    }

    public static Task WhenAll(this IEnumerable<Task> tasks)
    {
        return Task.WhenAll(tasks);
    }

    public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks)
    {
        return Task.WhenAll(tasks);
    }
}