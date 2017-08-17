using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WTextAnnotator.CS.Model
{
    public class Document
    {
        public String text;
        public ArrayList annotations;

        public static Document parseXML(String xmlStr)
        {
            Document doc = new Document ();

            XmlDocument docXML = new XmlDocument();
            docXML.LoadXml(xmlStr);

            XmlNode textNode = docXML.DocumentElement.SelectSingleNode("/deIdi2b2/TEXT");
            doc.text = textNode.InnerText;

            XmlNode tagsNode = docXML.DocumentElement.SelectSingleNode("/deIdi2b2/TAGS");

            doc.annotations = new ArrayList();
            foreach (XmlNode node in tagsNode.ChildNodes)
            {
                Annotation ann = new Annotation();
                ann.start = Int32.Parse(node.Attributes["start"].InnerText);
                ann.end = Int32.Parse(node.Attributes["end"].InnerText);
                ann.text = node.Attributes["text"].InnerText;
                ann.label = node.Name;
                doc.annotations.Add(ann);
            }

            return doc;
        }
    }

    
}
