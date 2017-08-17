using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTextAnnotator.CS.Model
{
    public class Annotation : IComparable
    {
        public int start;
        public int end;
        public string label;
        public string text;
        public DateTime datetime;
        public string source;

        public Annotation()
        {
        }

        public Annotation(int start, int end, string label, string text)
        {
            this.start = start;
            this.end = end;
            this.label = label;
            this.text = text;
        }

       

        public override string ToString()
        {
            return "["+label+" ("+start+" "+end+") "+text+"]";
        }

        public int CompareTo(object obj)
        {
           return this.CompareTo(obj);
        }
    }
}
