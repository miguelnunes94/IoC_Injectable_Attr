namespace Examples
{
    /// <summary>
    /// Error logging interface
    /// </summary>
    public interface ILoggerErrors
    {
        /// <summary>
        /// Logs the message as error.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogError(string message);
    }
}