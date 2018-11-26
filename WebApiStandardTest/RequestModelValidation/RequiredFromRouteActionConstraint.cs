using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStandardTest.RequestModelValidation
{
    //判断路由中参数的有效性

    public class RequiredFromRouteActionConstraint : IActionConstraint
    {
        private readonly string _parameter;
        public RequiredFromRouteActionConstraint(string parameter)
        {
            _parameter = parameter;
        }

        //We chose order with a high value to make sure our constraint runs last, 
        //especially after the built in framework constraints (some of which have order of 200).
        public int Order => 999; 

        public bool Accept(ActionConstraintContext context)
        {
            var subdataid = context.RouteContext.RouteData.Values[_parameter];
            //todo: 判断项目自己是否存在          
            return true;
        }
    }
}
