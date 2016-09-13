using System;

namespace Util.Helpers
{
    public static class SafeNavigationEntensionHelper
    {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (o == null)
                return null;

            return evaluator.Invoke(o);
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult defaultValue)
            where TInput : class
        {
            if (o == null)
                return defaultValue;

            return evaluator.Invoke(o);
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TInput : class
        {
            if (o == null)
            {
                var type = typeof(TResult);
                if (typeof(TResult).IsValueType && Nullable.GetUnderlyingType(type) == null)
                {
                    return (TResult)Activator.CreateInstance(type);
                }
                return (TResult)type.GetDefaultValue();
            }
            return evaluator.Invoke(o);
        }
    }
}
