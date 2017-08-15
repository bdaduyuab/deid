using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleDB
{
    class Program
    {
        static void Main(string[] args)
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
                 "select * from NLP_DOCS where NC_PT_MRN=598518";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string patientId = dr["NC_PT_MRN"].ToString();
                string reportId = dr["NC_REPORTID"].ToString();
                string reportType = dr["NC_TYPE"].ToString();
                string text = dr["NC_CLCONT"].ToString();
                System.Console.WriteLine(reportType);
            }



            dr.Dispose();
            cmd.Dispose();
            conn.Dispose();

            System.Console.ReadLine();

        }
    }
}
