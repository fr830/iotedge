// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

namespace Microsoft.Azure.Devices.Routing.Core.Query.Builtins
{
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.Azure.Devices.Routing.Core.Query.Types;

    public class Lower : Builtin
    {
        protected override BuiltinExecutor[] Executors => new[]
        {
            // lower(string)
            new BuiltinExecutor
            {
                InputArgs = new Args(typeof(string)),
                IsQueryValueSupported = true,
                ExecutorFunc = Create
            }
        };

        static Expression Create(Expression[] args, Expression[] contextArgs)
        {
            return Expression.Call(typeof(Lower).GetMethod("Runtime", BindingFlags.NonPublic | BindingFlags.Static), args);
        }

        // ReSharper disable once UnusedMember.Local
        static string Runtime(QueryValue input)
        {
            if (input?.ValueType != QueryValueType.String)
            {
                return Undefined.Instance;
            }

            string inputString = (string)input.Value;

            return !inputString.IsNullOrUndefined() ? inputString.ToLowerInvariant() : Undefined.Instance;
        }
    }
}
