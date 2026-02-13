using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using Xbim.IDS.Validator.Console.Internal;

namespace Xbim.IDS.Validator.Console
{
    public partial class CliOptions
    {
        /// <summary>
        /// Provides a means to associate the <see cref="ICommandAction"/> against a defined CommandLine <see cref="Command"/>
        /// </summary>
        public abstract class BaseCommand : Command
        {
            private readonly IServiceProvider provider;

            protected BaseCommand(IServiceProvider provider, string name, string description) : base(name, description)
            {
                this.provider = provider;
            }

            /// <summary>
            /// Sets the action / handler up to execute an <see cref="ICommandAction"/>
            /// </summary>
            /// <typeparam name="T">The defined Action type</typeparam>
            internal void SetAction<T>() where T : ICommandAction
            {

                this.SetAction(async (parseResult, cancellationToken) =>
                {
                    var command = provider.GetRequiredService<T>();
                    var result = await command.ExecuteActionAsync(parseResult, cancellationToken);
                    return result;
                });
            }
        }
    }
}
