using System;

namespace Aho
{

    public class Interval2 : Intervalable
    {

    private int start;
    private int end;

    public Interval2( int start,  int end)
    {
        this.start = start;
        this.end = end;
    }

    public int getStart()
    {
        return this.start;
    }

    public int getEnd()
    {
        return this.end;
    }

    public int size()
    {
        return end - start + 1;
    }

    public bool overlapsWith(Interval2 other)
    {
        return this.start <= other.getEnd() &&
               this.end >= other.getStart();
    }

    public bool overlapsWith(int point)
    {
        return this.start <= point && point <= this.end;
    }

    
    public bool equals(Object o)
    {
            if (!(o.GetType() == typeof(Intervalable)))
                return false;

            Intervalable other = (Intervalable)o;
        return this.start == other.getStart() &&
               this.end == other.getEnd();
    }

   

    public int CompareTo(object o)
    {

            
              if (!(o.GetType() == typeof(Intervalable)))
            return -1;
        
        Intervalable other = (Intervalable) o;
        int comparison = this.start - other.getStart();
        return comparison != 0 ? comparison : this.end - other.getEnd();
    }

    public override string ToString()
    {
        return this.start + ":" + this.end;
    }

     
    }
}

