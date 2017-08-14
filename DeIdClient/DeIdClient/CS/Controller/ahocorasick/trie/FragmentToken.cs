namespace Aho
{
    public class FragmentToken : Token
    {

    public FragmentToken(string fragment):base(fragment)
    {

    }

    
    public override bool isMatch()
    {
        return false;
    }

    public override Emit getEmit()
    {
        return null;
    }
}
}

   
