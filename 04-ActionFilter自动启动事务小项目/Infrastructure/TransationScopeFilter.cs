using Application;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure
{
    public class TransationScopeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 当前被执行的Action里面的描述信息：context.ActionDescriptor
            // 当前被执行的Actionmi里外面都有的描述信息： ControllerActionDescriptor
            // 为什么需要ControllerActionDescriptor呢？因为asp net core 不仅仅只有webapi/mvc，还有blazer、razor、pages等等，所以context.ActionDescriptor的类型是符合多元化的，但是具体是哪个需要自己指明，这里就指定了使用webapi/mvc的ControllerActionDescriptor
            // 所以 ControllerActionDescriptor是继承自context.ActionDescriptor。
            ControllerActionDescriptor ctrlActionDesc = context.ActionDescriptor as ControllerActionDescriptor;

            bool isTx = false;
            //如果方法或控制器上标记了NotTransationAttribute则为true
            bool hasNotTransationAttribute  = ctrlActionDesc.ControllerTypeInfo.GetCustomAttributes(typeof(NotTransationAttribute),false).Any()
             || ctrlActionDesc.MethodInfo.GetCustomAttributes(typeof(NotTransationAttribute), false).Any();

             isTx = !hasNotTransationAttribute;


            //标记则走事务
            if (isTx)
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await next();
                    if (result.Exception == null)
                    {
                        tx.Complete();
                    }
                }
            }
            else//否则直接放行
            {
                await next();
            }


            
        }
    }
}
