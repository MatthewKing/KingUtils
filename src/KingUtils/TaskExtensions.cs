using System.Threading.Tasks;

namespace KingUtils
{
    /// <summary>
    /// Extension methods for tasks.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Consumes the task and does nothing with it.
        /// Useful for fire-and-forget calls to asynchronous methods.
        /// </summary>
        /// <param name="task">The task to fire-and-forget.</param>
        public static void FireAndForget(this Task task)
        {
            // Do nothing.
            // This method is just to avoid warning CS4014.
        }
    }
}
