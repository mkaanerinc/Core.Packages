using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Rules;

/// <summary>
/// A utility class for running business rules asynchronously in a sequential manner.
/// This class executes each rule in the provided sequence, stopping execution if any rule throws an exception.
/// </summary>
public static class RuleRunner
{
    /// <summary>
    /// Executes a sequence of asynchronous business rules in order.
    /// If any rule throws an exception, the execution stops, and the exception is propagated.
    /// </summary>
    /// <param name="rules">A collection of asynchronous functions representing the business rules to execute.</param>
    /// <returns>A task that completes when all rules have been executed successfully, or throws an exception if any rule fails.</returns>
    /// <example>
    /// The following example demonstrates how to use <see cref="RunAsync"/> to execute a series of business rules:
    /// <code>
    /// await RuleRunner.RunAsync(
    ///     async () => await CheckIfUserExists(),
    ///     async () => await CheckIfBalanceIsSufficient(),
    ///     async () => await CheckIfAccountIsActive()
    /// );
    /// </code>
    /// In this example, <see cref="RunAsync"/> will execute each rule in sequence. If any rule (e.g., <c>CheckIfUserExists</c>) throws an exception,
    /// the subsequent rules (e.g., <c>CheckIfBalanceIsSufficient</c> and <c>CheckIfAccountIsActive</c>) will not be executed.
    /// </example>
    public static async Task RunAsync(params Func<Task>[] rules)
    {
        foreach (var rule in rules)
        {
            await rule();
        }
    }
}