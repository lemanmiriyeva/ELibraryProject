using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace ELibrary.Business.Utility.Interceptors
{
    public class MethodInterception:MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation){}
        protected virtual void OnAfter(IInvocation invocation){}
        protected virtual void OnException(IInvocation invocation,Exception exception){}
        protected virtual void OnSuccess(IInvocation invocation){}
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                isSuccess = false;
                OnException(invocation,exception);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
                OnAfter(invocation);
            }
        }
    }
}
