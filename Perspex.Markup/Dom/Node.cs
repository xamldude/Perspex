using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perspex.Dom
{
    public class Node : Child
    {
        public string Name { get; set; }
        public IEnumerable<Child> Children { get; set; }
    }
}
