﻿using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class ProductStatusValidator : AbstractValidator<ProductStatus>
    {
        public ProductStatusValidator() 
        {
           
        }
    }
}
