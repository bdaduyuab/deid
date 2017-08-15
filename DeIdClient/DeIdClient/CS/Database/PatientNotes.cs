using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTextAnnotator.CS.Database
{
    public class PatientNotes
    {
        public string patientId;
        public string noteId;
        public string noteType;
        public string text;


        public static ArrayList extractPatient(string mrn)
        {
            //jdbc: oracle: thin: medics_dev / medics_dev@vm - ccts - oak.hs.uab.edu:1521:cctsd
            string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=vm-ccts-oak.hs.uab.edu)(PORT=1521))"
             + "(CONNECT_DATA=(SERVICE_NAME=cctsd)));"
             + "User Id=medics_dev;Password=medics_dev;";


            OracleConnection conn = new OracleConnection(oradb); // C#
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText =
                 "select * from NLP_DOCS where NC_PT_MRN='"+ mrn + "'";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            ArrayList notes = new ArrayList();
            while (dr.Read())
            {

                PatientNotes note=new PatientNotes();

                note.patientId = dr["NC_PT_MRN"].ToString();
                note.noteId = dr["NC_REPORTID"].ToString();
                note.noteType = dr["NC_TYPE"].ToString();
                note.text = dr["NC_CLCONT"].ToString();


                notes.Add(note);
            }



            dr.Dispose();
            cmd.Dispose();
            conn.Dispose();

            return notes;
        }
    }
}
