using System;
using System.Data.Entity;
using System.Linq;

namespace DL
{
    public static class Db
    {
        private static IUnitOfWork StaticUnitOfWork { get; set; }
        private static DbContext StaticContext { get; set; }

        public static TOut Context<TOut>(Func<Context, TOut> action)
        {
            using (Context context = new Context())
            {
                return action.Invoke(context);
            }
        }

        public static void UnitOfWork(Action<IUnitOfWork> action)
        {
            UnitOfWork(uow => {
                action.Invoke(uow);
                return string.Empty;
            });
        }

        public static TOut UnitOfWork<TOut>(Func<IUnitOfWork, TOut> action)
        {
            if (StaticContext != null)
            {
                return action.Invoke(StaticUnitOfWork);
            }
            else
            {
                try
                {
                    using (DbContext context = new Context())
                    {
                        StaticContext = context;
                        StaticUnitOfWork = new UnitOfWork(StaticContext);
                        var o = action.Invoke(StaticUnitOfWork);
                        if (StaticContext.ChangeTracker.HasChanges())
                        {
                            StaticContext.SaveChanges();
                        }
                        StaticContext = null;
                        StaticUnitOfWork = null;
                        return o;
                    }
                }
                catch (Exception e)
                {
                    return (TOut)Convert.ChangeType(e.Message, typeof(TOut));
                }
            }
        }
    }
}
