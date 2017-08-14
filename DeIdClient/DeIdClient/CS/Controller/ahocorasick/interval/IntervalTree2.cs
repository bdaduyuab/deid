using System.Collections;
using System.Collections.Generic;

namespace Aho
{
    public class IntervalTree2
    {

        private IntervalNode2 rootNode = null;

        public IntervalTree2(ArrayList intervals)
        {
            this.rootNode = new IntervalNode2(intervals);
        }

        public ArrayList removeOverlaps(ArrayList intervals)
        {
            intervals.Sort(new IntervalableComparatorBySize());


            HashSet<Intervalable> removeIntervals = new HashSet<Intervalable>();

            foreach (Intervalable interval in intervals)
            {
                // If the interval was already removed, ignore it
                if (removeIntervals.Contains(interval))
                {
                    continue;
                }

                foreach (Intervalable rinterval in findOverlaps(interval))
                {
                    removeIntervals.Add(rinterval);
                }

                    

                // Remove all overallping intervals

            }

            // Remove all intervals that were overlapping
            foreach (Intervalable removeInterval in removeIntervals)
            {
                intervals.Remove(removeInterval);
            }

            intervals.Sort(new IntervalableComparatorByPosition());

            
            return intervals;
        }

        public ArrayList findOverlaps(Intervalable interval)
        {
            return rootNode.findOverlaps(interval);
        }

    }

}

