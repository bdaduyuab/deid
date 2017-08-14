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
using WTextAnnotator.CS.Model;

namespace WTextAnnotator
{
    public partial class MainGui : Form
    {
        public Trie trie;
        public AnnotationList annotationList = new AnnotationList();
        public string text;

        public MainGui()
        {
            InitializeComponent();
            initializeContextMenu();
            ItializeDictionary();

        }

        private void ItializeDictionary()
        {
            trie = new Trie();
            trie.finalize();
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
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("110-01.xml"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();

                    builder.Append(line);

                }

            }
            catch (Exception e1)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e1.Message);
            }

            Document doc = Document.parseXML(builder.ToString());
            this.text = doc.text;

            textArea.Text = this.text;

            annotatePHI(doc.annotations);



        }

        public void annotatePHI(Annotation annotation)
        {
            annotatePHI(annotation, false);
        }
        public void annotatePHI(ArrayList annotations)
        {
            annotatePHI(annotations, false);
        }

        public void annotatePHI(ArrayList annotations, bool refresh)
        {
            foreach (Annotation ann in annotations)
            {
                annotatePHI(ann, refresh);
            }
        }

        public void annotatePHI(Annotation ann, bool refresh)
        {


            if (!refresh)
            {
                ArrayList anns = annotationList.search(ann.start, ann.end);
                if (anns.Count > 0)
                    return;
                annotationList.Add(ann);
            }


            textArea.SelectionStart = ann.start;
            textArea.SelectionLength = ann.end - ann.start;
            textArea.SelectionBackColor = Color.Yellow;

            createRow(ann);
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

        private void loadAnnButton_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/WebTextAnnotator/loadtext?action=populatetext&id=0001_gs.xml");

            // Execute the request 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            WebHeaderCollection header = response.Headers;

            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                System.Console.WriteLine(responseText);
            }



        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EventHandler.dataGridView1_CellContentClick(this, sender, e);
        }


        private void buttonSort_Click(object sender, EventArgs e)
        {
            annotationList.Sort();
            refreshAll();
        }

        public void refreshAll()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            textArea.SelectionStart = 0;
            textArea.SelectionLength = textArea.Text.Length;
            textArea.SelectionBackColor = Color.White;

            annotatePHI(annotationList.annotations, true);
        }
        public void refreshTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            createRow(annotationList.annotations);
        }

        private void buttonImportDict_Click(object sender, EventArgs e)
        {
            Trie trie = new Trie();
            trie.trieConfig.setCaseInsensitive(true);

            ArrayList terms = FileUtil.loadFileLineByLine("lexicons\\case_insensitive\\dict1.lex");
            trie.addKeyword(terms);
            trie.finalize();

            ArrayList anns = trie.annotateText(textArea.Text, "PHI");

            annotatePHI(anns);
        }

        private void buttonAnnotateWord_Click(object sender, EventArgs e)
        {
            Trie trie = new Trie();
            trie.addKeyword(textBox1.Text.Trim());
            trie.finalize();
            ArrayList anns = trie.annotateText(textArea.Text, "PHI");
            annotatePHI(anns);
        }
    }
}
