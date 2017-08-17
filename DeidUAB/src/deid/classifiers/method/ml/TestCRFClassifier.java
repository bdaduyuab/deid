package deid.classifiers.method.ml;

import java.util.List;
import java.util.Properties;

import deid.Constants;
import deid.classifiers.method.ml.train.TrainClassifiersBinary;
import deid.document.IDocument;
import deid.document.RecordCorpus;
import deid.utils.Debug;
import edu.stanford.nlp.ie.AbstractSequenceClassifier;
import edu.stanford.nlp.ie.crf.CRFClassifier;
import edu.stanford.nlp.ling.CoreLabel;
import edu.stanford.nlp.util.Triple;

public class TestCRFClassifier {

	public static void main(String[] args) {
		AbstractSequenceClassifier<CoreLabel> classifier = importModelFile(TrainClassifiersBinary.binaryConfig.getTrainingModelFile());

		RecordCorpus recordSet = new RecordCorpus(Constants.TEST);
		recordSet.loadFolder(new String[] { "113-05.xml" },true);

		recordSet.loadObj();

		for (IDocument doc : recordSet.documents) {
			List<Triple<String, Integer, Integer>> triples = classifier.classifyToCharacterOffsets(doc.getText());
			for (Triple<String, Integer, Integer> trip : triples) {
				int start = trip.second();
				int end =  trip.third();
				String label = trip.first();
				String text=doc.unTaggedText.substring(start, end);
				Debug.print(text);

			}
		}

	}

	public static AbstractSequenceClassifier<CoreLabel> importModelFile(String modelFilePath) {

		AbstractSequenceClassifier<CoreLabel> classifier = null;
		try {
			Properties props = new Properties();
			// props.setProperty("tokenizerFactory",
			// "edu.stanford.nlp.process.WhitespaceTokenizer");
			classifier = CRFClassifier.getClassifier(modelFilePath, props);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return classifier;
	}

}
