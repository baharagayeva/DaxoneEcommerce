using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationMessages
{
    public static class ValidationMessages
    {
        public static readonly string CanNotBeEmpty = "Field can not be empty";
        public static readonly string HasMaxLength = "Field can not be more than 50 symbols";
        public static readonly string HasLength = "Field must have minimum 40 and maximum 200 symbols";
        public static readonly string FileCanNotBeEmpty = "File can not be empty";
        public static readonly string CanNotBeNegative = "Value can not be negative";
    }
}
