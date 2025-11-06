using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL.Infrastructure
{
    public static class CoreValidator
    {
        public static bool CheckIfStringIsNullOrEmpty(string input) => String.IsNullOrEmpty(input);
    }
}
