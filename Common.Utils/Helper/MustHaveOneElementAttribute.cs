using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Common.Utils.Helper
{
    public class MustHaveOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is IList { Count: > 0 };
        }
    }
}
