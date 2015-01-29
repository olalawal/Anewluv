using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Services.DependencyResolution.Ninject.predicates
{
    public static class Predicates
    {
        public static bool TargetHas<T>(IContext context)
        {
            return TargetHas<T>(context, false);
        }

        public static bool TargetHas<T>(IContext context, bool inherit)
        {
            var target = context.Request.Target;
            return target != null && target.IsDefined(typeof(T), inherit);
        }
    }
}
