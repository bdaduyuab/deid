package deid.anns;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

public class AnnotationGroup extends IAnnotation{
	
	public Pattern pattern;
	public List<IAnnotation> annotations=new ArrayList<IAnnotation>();
	
	public int getStart(){
		return annotations.get(0).start;
	}
	public int getEnd(){
		return annotations.get(annotations.size()-1).end;
	}
}
