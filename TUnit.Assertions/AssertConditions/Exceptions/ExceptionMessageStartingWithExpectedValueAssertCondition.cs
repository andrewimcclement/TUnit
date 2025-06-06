﻿using TUnit.Assertions.Extensions;
using TUnit.Assertions.Helpers;

namespace TUnit.Assertions.AssertConditions.Exceptions;

public class ExceptionMessageStartingWithExpectedValueAssertCondition<TException>(string expected, StringComparison stringComparison)
    : ExpectedValueAssertCondition<TException, string>(expected)
where TException : Exception
{
    protected override string GetExpectation()
        => $"message to start with {Formatter.Format(expected).TruncateWithEllipsis(100)}";

    protected override ValueTask<AssertionResult> GetResult(TException? actualValue, string? expectedValue)
    {
        if (actualValue?.Message is null)
        {
            return AssertionResult
                .FailIf(expectedValue is not null,
                    "the exception message was null");
        }

        return AssertionResult
            .FailIf(expectedValue is null,
                "expected value was null")
            .OrFailIf(!actualValue.Message.StartsWith(expectedValue!, stringComparison),
                $"found message {Formatter.Format(actualValue.Message).TruncateWithEllipsis(100)}");
    }
}