package deid.classifiers.dict;

import java.util.List;

import deid.anns.IAnnotation;
import deid.document.IDocument;
import deid.utils.dict.HumanNames;

public class DictionaryClassifier {
	
	private static DictionaryClassifier dicClassifier;
	
	public static DictionaryClassifier getDicClassifier(){
		if(dicClassifier==null){
			dicClassifier=new DictionaryClassifier("tokenDict", HumanNames.HUMAN_NAME_LEXICON_SELECTED);
		}
		return dicClassifier;
	}
	
	
	public Dictionary dic;
	public String filePath;
	public String name;
	
	public DictionaryClassifier(String name,String filePath) {
		this.filePath = filePath;
		this.name = name;
		dic=new Dictionary(new String[]{filePath}, true, true, true);
	}
	
	public List<IAnnotation> annotate(IDocument doc){
		List<IAnnotation> anns=dic.matchDoc(doc);
		for(IAnnotation ann:anns){
			ann.label="PHI";
			ann.source="DICT";
		}
		
		return anns;
	}
	

}
