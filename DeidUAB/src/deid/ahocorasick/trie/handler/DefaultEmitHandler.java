package deid.ahocorasick.trie.handler;

import java.util.ArrayList;
import java.util.List;

import deid.ahocorasick.trie.Emit;

public class DefaultEmitHandler implements EmitHandler {

    private List<Emit> emits = new ArrayList<>();

    @Override
    public void emit(Emit emit) {
        this.emits.add(emit);
    }

    public List<Emit> getEmits() {
        return this.emits;
    }

}
