namespace Aho
{
    public class MatchToken : Token
    {

    private Emit emit;

    public MatchToken(string fragment, Emit emit) : base(fragment)
    {
    
        this.emit = emit;
    }

    public override bool isMatch()
    {
        return true;
    }

    
    public override Emit getEmit()
    {
        return this.emit;
    }

}

}

    