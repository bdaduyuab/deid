using Aho;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WTextAnnotator.CS.Controller;
using WTextAnnotator.CS.Database;
using WTextAnnotator.CS.Model;

namespace WTextAnnotator
{
    public partial class MainGui : Form
    {
        public static string RESOURCE_DIR = "C:/bdaduy/Codes/UAB/git/deid/DeIdClient/DeIdClient/resources";
        public AnnotationList annotationList = new AnnotationList();
        public Document document;

        public ArrayList notes;

        public MainGui()
        {
            InitializeComponent();
            initializeContextMenu();

        }


        public void initializeContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem phiItemNew = new MenuItem();
            phiItemNew.Text = "&PHI";
            phiItemNew.Click += new System.EventHandler(menuItemPHI_Click);
            contextMenu.MenuItems.Add(phiItemNew);

            phiItemNew = new MenuItem();
            phiItemNew.Text = "&Delete";
            phiItemNew.Click += new System.EventHandler(menuItemDelete_Click);
            contextMenu.MenuItems.Add(phiItemNew);

            textArea.ContextMenu = contextMenu;
        }

        private void menuItemPHI_Click(object sender, EventArgs e)
        {
            EventHandler.menuItemPHI_Click(this, sender, e);
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            EventHandler.menuItemDelete_Click(this, sender, e);
        }



        private void buttonLoadText_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            try
            {

                string filePath = RESOURCE_DIR + "/110-01.xml";
                if (File.Exists(filePath))
                {
                    // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        // Read the stream to a string, and write the string to the console.
                        String line = sr.ReadToEnd();

                        builder.Append(line);

                    }

                    document = Document.parseXML(builder.ToString());
                    

                    textArea.Text = document.text;

                    
                    //annotatePHI(doc.annotations);
                }
                else
                {
                    log("The file is not be found:" + filePath);

                }
            }
            catch (Exception e1)
            {
                log("The file could not be read:");
                log(e1.Message);
            }
        }


        private void buttonLoadAnnotation_Click(object sender, EventArgs e)
        {
            annotationList.AddRange(document.annotations);
            refreshEditor();
            refreshTable();
        }



        public void createRow(ArrayList annotations)
        {
            foreach (Annotation ann in annotations)
            {
                createRow(ann);
            }
        }

        public void createRow(Annotation ann)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = ann.start;
            row.Cells[1].Value = ann.end;
            row.Cells[2].Value = "Manual";
            row.Cells[3].Value = DateTime.Now.ToString("h:mm:ss tt");
            row.Cells[4].Value = ann.text;
            dataGridView1.Rows.Add(row);
        }

        public void removePHIFromEditor(Annotation ann)
        {
            textArea.SelectionStart = ann.start;
            textArea.SelectionLength = ann.end - ann.start;
            textArea.SelectionBackColor = Color.White;
        }
        public void removePHIFromEditor(ArrayList annotations)
        {
            foreach (Annotation ann in annotations)
            {
                removePHIFromEditor(ann);
            }
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EventHandler.dataGridView1_CellContentClick(this, sender, e);
        }


        private void buttonSort_Click(object sender, EventArgs e)
        {
            annotationList.Sort();
            refreshEditor();

            refreshTable();
        }

        public void refreshAll()
        {
            refreshEditor();
            refreshTable();
        }

        public void refreshEditor()
        {
            textArea.SelectionStart = 0;
            textArea.SelectionLength = textArea.Text.Length;
            textArea.SelectionBackColor = Color.White;
            markPHI(annotationList.annotations);
        }


        public void refreshTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            createRow(annotationList.annotations);
        }

        public void markPHI(ArrayList annotations)
        {
            foreach (Annotation ann in annotations)
            {
                markPHI(ann);
            }
        }

        public void markPHI(Annotation ann)
        {
            textArea.SelectionStart = ann.start;
            textArea.SelectionLength = ann.end - ann.start;
            textArea.SelectionBackColor = Color.Yellow;

        }

        private void buttonImportDict_Click(object sender, EventArgs e)
        {
            Trie trie = new Trie();
            trie.trieConfig.setCaseInsensitive(true);

            ArrayList terms = FileUtil.loadFileLineByLine(RESOURCE_DIR + "/lexicons/case_insensitive/dict1.lex");
            trie.addKeyword(terms);
            trie.finalize();

            ArrayList anns = trie.annotateText(textArea.Text, "PHI");

            markPHI(anns);
        }

        private void buttonAnnotateWord_Click(object sender, EventArgs e)
        {
            Trie trie = new Trie();
            trie.addKeyword(textBoxKeyword.Text.Trim());
            trie.finalize();
            ArrayList anns = trie.annotateText(textArea.Text, "PHI");
            markPHI(anns);
        }

        private void buttonLoadPatient_Click(object sender, EventArgs e)
        {
            notes = PatientNotes.extractPatient(textBoxPatientId.Text);
            foreach (PatientNotes note in notes)
            {
                comboBoxNotes.Items.Add(note.noteId + " " + note.noteType);
            }
            comboBoxNotes.SelectedIndex = 0;

        }

        private void ComboBoxNotes_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxNotes.SelectedIndex >= 0)
            {
                PatientNotes note = (PatientNotes)notes[comboBoxNotes.SelectedIndex];
                textArea.Text = note.text;
            }

        }

        public static void log(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }

        private void buttonRunService_Click(object sender, EventArgs e)
        {

            var request = HttpWebRequest.Create("http://localhost:8080/DeIdService/MainServlet");

            var postData = textArea.Text;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseString);
            XmlNodeList nodeList = doc.SelectNodes("//RESPONSE/ANNOTATION");

            foreach (XmlNode node in nodeList)
            {
                int start = Int32.Parse(node.Attributes["start"].Value);
                int end = Int32.Parse(node.Attributes["end"].Value);
                string text=node.Attributes["text"].Value;
                string label = node.Attributes["label"].Value;
                Annotation ann = new Annotation(start,end,label,text);
                annotationList.Add(ann);

                
            }

            refreshAll();
            //log(responseString);

            
        }


    }
}
