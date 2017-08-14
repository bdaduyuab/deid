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
        public String label;
        public String text;

        public Annotation()
        {
        }

        public Annotation(int start, int end, String label, String text)
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
