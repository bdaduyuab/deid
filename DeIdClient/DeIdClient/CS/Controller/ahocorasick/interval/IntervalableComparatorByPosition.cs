using System;
using System.Collections.Generic;

namespace Aho
{
    public class IntervalableComparatorByPosition : Comparer<Intervalable> {


    public override int Compare(Intervalable intervalable, Intervalable intervalable2)
    {
        return intervalable.getStart() - intervalable2.getStart();
    }


    }
    

}
