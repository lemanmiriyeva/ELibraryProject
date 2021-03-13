using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Castle.DynamicProxy;
using ELibrary.Business.Utility.Interceptors;

namespace ELibrary.Business.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect:MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope scope=new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    scope.Complete();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }
    }
}
