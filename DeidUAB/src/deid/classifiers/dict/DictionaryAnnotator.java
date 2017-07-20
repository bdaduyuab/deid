package deid.classifiers.dict;

import java.util.List;

import deid.anns.IAnnotation;
import deid.classifiers.cache.CacheAnnotation;
import deid.classifiers.ml.CRFUtil;
import deid.document.IDocument;
import deid.utils.intervaltree.IntervalTree;
import edu.stanford.nlp.ie.AbstractSequenceClassifier;
import edu.stanford.nlp.ling.CoreLabel;

public class DictionaryAnnotator {
	public static void annotate(List<IDocument> docs) {

		AbstractSequenceClassifier<CoreLabel> classifier = null;

		for (IDocument doc : docs) {
			List<IAnnotation> anns = DictionaryClassifier.getDicClassifier().annotate(doc);

			for (IAnnotation ann : anns) {
				doc.testTokenRegistry.addInterval(ann.start, ann.end, ann);
			}

		}

	}
}
