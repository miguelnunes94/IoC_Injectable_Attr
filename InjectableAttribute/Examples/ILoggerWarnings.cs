namespace Examples
{
    /// <summary>
    /// Warning logger interface
    /// </summary>
    public interface ILoggerWarnings
    {
        /// <summary>
        /// Logs the message as warning.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogWarning(string message);
    }
}