
using System.Collections;
using System.Collections.Generic;

namespace Aho
{
    public class IntervalNode2
    {

        public enum Direction { LEFT, RIGHT }

        private IntervalNode2 left = null;
        private IntervalNode2 right = null;
        private int point;
        private ArrayList intervals = new ArrayList();

        public IntervalNode2(ArrayList intervals)
        {
            this.point = determineMedian(intervals);

            ArrayList toLeft = new ArrayList();
            ArrayList toRight = new ArrayList();

            foreach (Intervalable interval in intervals)
            {
                if (interval.getEnd() < this.point)
                {
                    toLeft.Add(interval);
                }
                else if (interval.getStart() > this.point)
                {
                    toRight.Add(interval);
                }
                else
                {
                    this.intervals.Add(interval);
                }
            }

            if (toLeft.Count > 0)
            {
                this.left = new IntervalNode2(toLeft);
            }
            if (toRight.Count > 0)
            {
                this.right = new IntervalNode2(toRight);
            }
        }

        public int determineMedian(ArrayList intervals)
        {
            int start = -1;
            int end = -1;
            foreach (Intervalable interval in intervals)
            {
                int currentStart = interval.getStart();
                int currentEnd = interval.getEnd();
                if (start == -1 || currentStart < start)
                {
                    start = currentStart;
                }
                if (end == -1 || currentEnd > end)
                {
                    end = currentEnd;
                }
            }
            return (start + end) / 2;
        }

        public ArrayList findOverlaps(Intervalable interval)
        {

            ArrayList overlaps = new ArrayList();

            if (this.point < interval.getStart())
            { // Tends to the right
                addToOverlaps(interval, overlaps, findOverlappingRanges(this.right, interval));
                addToOverlaps(interval, overlaps, checkForOverlapsToTheRight(interval));
            }
            else if (this.point > interval.getEnd())
            { // Tends to the left
                addToOverlaps(interval, overlaps, findOverlappingRanges(this.left, interval));
                addToOverlaps(interval, overlaps, checkForOverlapsToTheLeft(interval));
            }
            else
            { // Somewhere in the middle
                addToOverlaps(interval, overlaps, this.intervals);
                addToOverlaps(interval, overlaps, findOverlappingRanges(this.left, interval));
                addToOverlaps(interval, overlaps, findOverlappingRanges(this.right, interval));
            }

            return overlaps;
        }

        protected void addToOverlaps(Intervalable interval, ArrayList overlaps, ArrayList newOverlaps)
        {
            foreach (Intervalable currentInterval in newOverlaps)
            {
                if (!currentInterval.Equals(interval))
                {
                    overlaps.Add(currentInterval);
                }
            }
        }

        protected ArrayList checkForOverlapsToTheLeft(Intervalable interval)
        {
            return checkForOverlaps(interval, Direction.LEFT);
        }

        protected ArrayList checkForOverlapsToTheRight(Intervalable interval)
        {
            return checkForOverlaps(interval, Direction.RIGHT);
        }

        protected ArrayList checkForOverlaps(Intervalable interval, Direction direction)
        {

            ArrayList overlaps = new ArrayList();
            foreach (Intervalable currentInterval in this.intervals)
            {
                switch (direction)
                {
                    case Direction.LEFT:
                        if (currentInterval.getStart() <= interval.getEnd())
                        {
                            overlaps.Add(currentInterval);
                        }
                        break;
                    case Direction.RIGHT:
                        if (currentInterval.getEnd() >= interval.getStart())
                        {
                            overlaps.Add(currentInterval);
                        }
                        break;
                }
            }
            return overlaps;
        }


        protected ArrayList findOverlappingRanges(IntervalNode2 node, Intervalable interval)
        {
            if (node != null)
            {
                return node.findOverlaps(interval);
            }
            return new ArrayList();
        }

    }

}
