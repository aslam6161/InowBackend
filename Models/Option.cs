using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InowBackend.Models
{
    public class Option
    {
        public Option()
        {
            SelectedOptions = new List<int>();
        }
        public List<int> SelectedOptions { get; set; }
        public int FileSize { get; set; }
    }

}
