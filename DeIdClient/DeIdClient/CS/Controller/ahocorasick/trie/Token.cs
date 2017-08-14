namespace Aho
{
    public abstract class Token
    {

        private string fragment;

        public Token(string fragment)
        {
            this.fragment = fragment;
        }

        public string getFragment()
        {
            return this.fragment;
        }

        public abstract bool isMatch();

        public abstract Emit getEmit();

    }

}

