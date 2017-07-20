package deid.ahocorasick.trie;

import java.util.Collection;

public class TestClass {

	public static void main(String[] args) {
		Trie trie = Trie.builder()
				.removeOverlaps()
		        .addKeyword("Heart failure")
		        .addKeyword("his")
		        .addKeyword("she")
		        .addKeyword("he")
		        .build();
		    Collection<Emit> emits = trie.parseText("heart failure patient x");
		    
		    for(Emit e:emits){
		    	System.out.print(e);
		    }
	}

}
