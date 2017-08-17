using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WTextAnnotator.CS.Model;

namespace WTextAnnotator
{
    class EventHandler
    {

        public static void menuItemPHI_Click(MainGui gui, object sender, EventArgs e)
        {

            int start = gui.textArea.SelectionStart;
            int end = start + gui.textArea.SelectionLength;
            String text = gui.textArea.SelectedText;
            String trimStartText = text.TrimStart();
            start += (text.Length - trimStartText.Length);
            String trimEndText = text.TrimEnd();

            end -= (text.Length - trimEndText.Length);

            if (end > start)
            {
                Annotation ann = new Annotation(start, end, "PHI", text.Trim());
                gui.annotationList.Add(ann);
                gui.markPHI(ann);
            }
        }

        public static void menuItemDelete_Click(MainGui gui, object sender, EventArgs e)
        {
            int start = gui.textArea.SelectionStart;
            int end = start + gui.textArea.SelectionLength;
            ArrayList removedAnns = gui.annotationList.Remove(start, end);
            gui.removePHIFromEditor(removedAnns);
            gui.refreshTable();

        }

        public static void dataGridView1_CellContentClick(MainGui gui,object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gui.dataGridView1.Rows[e.RowIndex];
                if (row.IsNewRow)
                    return;

                int start = Int32.Parse(row.Cells[0].Value.ToString());
                int end = Int32.Parse(row.Cells[1].Value.ToString());

                gui.textArea.SelectionStart = start;
                gui.textArea.SelectionLength = end - start;
                gui.textArea.Focus();

                //Console.WriteLine(start+" "+end);

            }
        }
    }
}
