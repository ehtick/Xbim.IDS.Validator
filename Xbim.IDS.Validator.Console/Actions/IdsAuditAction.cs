using Microsoft.Extensions.Logging;
using System.CommandLine;
using Xbim.IDS.Validator.Console.Internal;
using Xbim.IDS.Validator.Core.Interfaces;

namespace Xbim.IDS.Validator.Console.Actions
{
    /// <summary>
    /// Audits IDS files against the IDS 1.0 standards using ids-lib
    /// </summary>
    internal class IdsAuditAction : ICommandAction
    {
        private readonly IIdsValidator idsValidator;
        private readonly ILogger<IdsAuditAction> logger;

        public IdsAuditAction(IIdsValidator idsValidator, ILogger<IdsAuditAction> logger)
        {
            this.idsValidator = idsValidator;
            this.logger = logger;
        }

        public async Task<int> ExecuteActionAsync(ParseResult parseResult, CancellationToken cancellationToken)
        {
            var ids = parseResult.GetValue(CliOptions.IdsFilesArgument);
            var verbosity = parseResult.GetValue(CliOptions.VerbosityOption);

            return await Execute(ids, verbosity);
        }

        public Task<int> Execute(FileInfo[] idsFiles, Verbosity verbosity)
        {
            var console = new ConsoleLogger(verbosity);
            IdsLib.Audit.Status result = IdsLib.Audit.Status.Ok;
            foreach (var idsFile in idsFiles)
            {
                console.WriteInfoLine(ConsoleColor.White, "Auditing {0}", idsFile);
                var res = idsValidator.ValidateIDS(idsFile.FullName, logger);
                console.WriteInfoLine(ConsoleColor.White, "Result: {1} for {0}", idsFile, res);
                result |= res;
            }

            return Task.FromResult((int)result);
        }
    }
}
