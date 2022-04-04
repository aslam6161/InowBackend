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
        public int NumericP { get; set; }
        public int AlphaNumericP { get; set; }
        public int FloatP { get; set; }
    }

}
