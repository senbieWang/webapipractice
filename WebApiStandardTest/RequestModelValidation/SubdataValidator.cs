using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStandardTest.RequestModelValidation
{
    public class SubdataValidator : AbstractValidator<V2.Models.Subdata>
    {
        public SubdataValidator()
        {        
            RuleFor(x => x.SubdataName).Length(10,20).WithMessage("state must between 10 and 20.");
            RuleFor(x => x.State).InclusiveBetween(5, 10).WithMessage("length must between 5 and 10.");           
        }
    }
}
