using System.Collections.Generic;

namespace Aho
{
    public class IntervalableComparatorBySize : Comparer<Intervalable> {

    public override int Compare(Intervalable intervalable, Intervalable intervalable2)
    {
        int comparison = intervalable2.size() - intervalable.size();
        if (comparison == 0)
        {
            comparison = intervalable.getStart() - intervalable2.getStart();
        }
        return comparison;
    }

}
}


