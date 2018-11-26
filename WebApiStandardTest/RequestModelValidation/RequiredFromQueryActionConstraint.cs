using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStandardTest.RequestModelValidation
{
    public class RequiredFromQueryActionConstraint : IActionConstraint
    {      
        private readonly string _parameter;
        public RequiredFromQueryActionConstraint(string parameter)
        {
            _parameter = parameter;
        }

        //We chose order with a high value to make sure our constraint runs last, 
        //especially after the built in framework constraints (some of which have order of 200).
        public int Order => 999;

        public bool Accept(ActionConstraintContext context)
        {
            //如果存在该参数就做相应的判断
            if (context.RouteContext.HttpContext.Request.Query.ContainsKey(_parameter))
            {
                var para = context.RouteContext.HttpContext.Request.Query[_parameter];

                //todo:相应的处理
            }         
            return true;
        }
    }
}
