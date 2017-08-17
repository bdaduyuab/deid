using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTextAnnotator.CS.Model;

namespace WTextAnnotator.CS.Controller
{
    public class AnnotationList
    {
        public ArrayList annotations = new ArrayList();

        public void Add(Annotation ann)
        {
            annotations.Add(ann);
        }
        public void AddRange(ArrayList anns)
        {
            annotations.AddRange(anns);
        }

        public ArrayList search(int start, int end)
        {
            ArrayList results = new ArrayList();
            foreach (Annotation ann in annotations)
            {
                if (!((ann.start < start && ann.end <= start) || (ann.end > end && ann.start >= end)))
                {
                    results.Add(ann);
                }
            }
            return results;
        }
        public ArrayList Remove(int start, int end)
        {
            ArrayList anns = search(start,end);
            foreach (Annotation ann in anns)
            {
                annotations.Remove(ann);
            }
            return anns;
        }

        public void Sort()
        {
            annotations.Sort(new PositionComparer());
        }
    }

    public class PositionComparer : Comparer<Annotation>
    {


        public override int Compare(Annotation a1, Annotation a2)
        {
            return a1.start - a2.start;
        }


    }
}
