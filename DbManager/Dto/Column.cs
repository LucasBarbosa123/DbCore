using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagerService.Dto
{
    public class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<string> Constraigts { get; set; }
    }
}
