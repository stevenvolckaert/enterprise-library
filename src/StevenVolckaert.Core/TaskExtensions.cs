#if !NET35
namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides extension methods for <see cref="Task"/> and <see cref="Task{TResult}"/> instances.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Sorts a sequence of tasks in the order that they will complete.
        /// </summary>
        /// <typeparam name="TResult">The type returned by the tasks of <paramref name="tasks"/>.</typeparam>
        /// <param name="tasks">The sequence of tasks to order.</param>
        /// <returns>A sequence of tasks, sorted in the order that they will complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tasks"/> is <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "InCompletion", Justification = "Term is cased correctly.")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Rule does not apply to the task-based asynchronous programming pattern.")]
        public static IEnumerable<Task<TResult>> InCompletionOrder<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            if (tasks == null)
                throw new ArgumentNullException(nameof(tasks));

            var inputTasks = tasks.ToList();
            var sources = inputTasks.Select(x => new TaskCompletionSource<TResult>()).ToList();

            int nextTaskIndex = -1;

            foreach (var inputTask in inputTasks)
            {
                inputTask.ContinueWith(
                    completed =>
                    {
                        var source = sources[Interlocked.Increment(ref nextTaskIndex)];
                        if (completed.IsFaulted)
                            source.TrySetException(completed.Exception.InnerExceptions);
                        else if (completed.IsCanceled)
                            source.TrySetCanceled();
                        else
                            source.TrySetResult(completed.Result);
                    },
                    cancellationToken: CancellationToken.None,
                    continuationOptions: TaskContinuationOptions.ExecuteSynchronously,
                    scheduler: TaskScheduler.Default
                );
            }

            return sources.Select(x => x.Task);
        }
    }
}
#endif
