using System.CommandLine;
using System.CommandLine.Invocation;

namespace Xbim.IDS.Validator.Console.Internal
{
    /// <summary>
    /// An interface defining an Action that can be executed asynchronously to implement a CLI command.
    /// </summary>
    internal interface ICommandAction
    {
        /// <summary>
        /// Executes an Action using the <paramref name="result"/> from the parsed command
        /// </summary>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> ExecuteActionAsync(ParseResult result, CancellationToken cancellationToken);
    }
}
