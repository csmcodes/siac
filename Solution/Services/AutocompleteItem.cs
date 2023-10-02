using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class AutocompleteItem
    {
        public string label { get; set; }
        public string value { get; set; }
        public string info { get; set; }

        public AutocompleteItem()
        { }

        public AutocompleteItem(string label, string value, string info)
        {
            this.label = label;
            this.value = value;
            this.info = info;
        }
    }

}
