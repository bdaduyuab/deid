package deid.classifiers.method.ml.train;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import deid.Constants;
import deid.anns.IAnnotation;
import deid.document.IDocument;
import deid.document.IToken;
import deid.utils.Debug;
import deid.utils.FileUtil;
import deid.utils.StringFormat;

public class CRFBinaryConfig {
	public String rootDir = Constants.ML_MODELS;

	public String experimentName;

	public CRFBinaryConfig(String experimentName) {
		super();
		this.experimentName = experimentName;

	}

	public String getRunDir() {
		return rootDir + "/" + experimentName;
	}

	public String getOutputdir() {
		return getRunDir() + "/outputs";
	}

	public String getTrainingDataDir() {
		return getRunDir() + "/" + "TrainingData";
	}

	public String getTrainingPropFile() {
		return getRunDir() + "/" + "standford.ner.prop";
	}

	public String getTrainingModelFile() {
		return getRunDir() + "/" + "standford.ser.gz";
	}

	public void prepareDirBinary(List<IDocument> documents, String configModelFile, String gazetteFile) {
		Debug.print(FileUtil.getAbsolutePath(getRunDir()));
		File dir = new File(rootDir);
		dir.mkdirs();

		dir = new File(getTrainingDataDir());
		dir.mkdirs();

		List<String> trainFileList = new ArrayList<>();
		for (IDocument doc : documents) {
			List<IToken> tokens = doc.getTokens();
			StringBuffer buf = new StringBuffer();
			for (IToken token : tokens) {

				String label = token.goldLabel;
				// if(!label.equals("O"))
				// label="PHI";

				buf.append(token.normText + "\t" + label + "\n");
			}

			String outputFilePAth = getTrainingDataDir() + "/" + doc.fileName + ".tsv";
			FileUtil.writeFile(buf.toString(), outputFilePAth, false);

			trainFileList.add(outputFilePAth);

		}

		Debug.print("trainFileList:" + trainFileList.size());

		StringBuffer buf = new StringBuffer();
		buf.append("trainFileList = " + StringFormat.listToString(trainFileList, ",") + "\n");
		buf.append("serializeTo = " + getTrainingModelFile() + "\n");
		if (gazetteFile != null) {
			buf.append("cleanGazette=true\n");
			File gazFile = new File(gazetteFile);
			buf.append("gazette=" + gazFile.getAbsolutePath().replaceAll("\\\\", "/") + "\n");

		}

		buf.append(FileUtil.readFile(configModelFile));
		FileUtil.writeFile(buf.toString(), getTrainingPropFile(), false);
	}
}
