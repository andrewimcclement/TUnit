﻿#nullable disable

using System.Runtime.CompilerServices;
using TUnit.Assertions.AssertConditions.Collections;
using TUnit.Assertions.AssertConditions.Interfaces;
using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.Extensions;

public static class CollectionsIsNotExtensions
{
    public static InvokableValueAssertionBuilder<TActual> IsNotEquivalentTo<TActual, TInner>(this IValueSource<TActual> valueSource, IEnumerable<TInner> expected, IEqualityComparer<TInner> equalityComparer = null, [CallerArgumentExpression(nameof(expected))] string doNotPopulateThisValue = null)
        where TActual : IEnumerable<TInner>
    {
        return valueSource.RegisterAssertion(new EnumerableNotEquivalentToExpectedValueAssertCondition<TActual, TInner>(expected, equalityComparer)
            , [doNotPopulateThisValue]);
    }
    
    public static InvokableValueAssertionBuilder<IEnumerable<TInner>> IsNotEmpty<TInner>(this IValueSource<IEnumerable<TInner>> valueSource)
    {
        return valueSource.RegisterAssertion(new EnumerableCountNotEqualToExpectedValueAssertCondition<IEnumerable<TInner>, TInner>(0)
            , []);
    }
}