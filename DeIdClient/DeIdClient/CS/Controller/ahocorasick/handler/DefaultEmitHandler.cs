using System.Collections;

namespace Aho
{
    public class DefaultEmitHandler : EmitHandler
    {

    private ArrayList emits = new ArrayList();


    public void emit(Emit emit)
    {
        this.emits.Add(emit);
    }

    public ArrayList getEmits()
    {
        return this.emits;
    }

}

}

   