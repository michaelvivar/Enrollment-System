using System;
using System.Data.Entity;
using System.Linq;

namespace DL
{
    public static class Db
    {
        public static TOut Context<TOut>(Context context, Func<Context, TOut> action)
        {
            var o = action.Invoke(context);
            return o;
        }

        public static TOut UnitOfWork<TOut>(Context context, Func<IUnitOfWork, TOut> action)
        {
            IUnitOfWork unit = new UnitOfWork(context);
            var o = action.Invoke(unit);
            return o;
        }
    }
}
