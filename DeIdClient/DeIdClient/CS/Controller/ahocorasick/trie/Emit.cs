
namespace Aho
{
    public class Emit : Interval2 , Intervalable
    {

    private string keyword;

    public Emit( int start,  int end,  string keyword) : base(start, end)
    {
        
        this.keyword = keyword;
    }

    public string getKeyword()
    {
        return this.keyword;
    }

    
    public override string ToString()
    {
        return base.ToString() + "=" + this.keyword;
    }

}

}

  