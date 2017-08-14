using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTextAnnotator.CS.Controller
{
    class FileUtil
    {
        public static ArrayList loadFileLineByLine(string filePath)
        {
            ArrayList terms = new ArrayList();

            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =   new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                terms.Add(line);
            }

            file.Close();

            return terms;
        }
    }
}
