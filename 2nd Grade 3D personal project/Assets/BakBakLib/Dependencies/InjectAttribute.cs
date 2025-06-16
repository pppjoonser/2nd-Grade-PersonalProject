using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakBak.Dependencies
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
    }
}
