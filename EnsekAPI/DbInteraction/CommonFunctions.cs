
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace EnsekAPI.DbInteraction
{
    public class CommonFunctions
    {

        //Method to read the csv file
        public static List<MeterReading> ReadFile(string JsonString)
        {


            List<MeterReading> meterlist = new List<MeterReading>();


            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(JsonString))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {

                        dr[i] = rows[i];

                    }


                    //validation check for duplicate value
                    //• You should not be able to load the same entry twice
                   // A meter reading must be associated with an Account ID to be deemed valid

                   int val = Convert.ToInt32(dr["AccountId"].ToString());
              
              

                    if (int.TryParse(dr["MeterReadValue"].ToString(), out int value))
                    {

                        bool contains = (dt.AsEnumerable().Any(row => val == (Convert.ToInt32(row.Field<string>("AccountId")))) && dt.AsEnumerable().Any(row => value == (Convert.ToInt32(row.Field<string>("MeterReadValue")))));

                        if (contains == false)
                    {
                        dt.Rows.Add(dr);
                    }
                    }
                }

                foreach (DataRow row in dt.Rows)
                {

                    MeterReading meterReadings = new MeterReading();
                    meterReadings.AccountId = Convert.ToInt32(row["AccountId"].ToString());
                   meterReadings.MeterReadValue = Convert.ToInt32(row["MeterReadValue"].ToString());
                    
                   DateTime d1t = DateTime.ParseExact(row["MeterReadingDateTime"].ToString(), "dd/MM/yyyy HH:mm", null);
                   
                    meterReadings.MeterReadingDateTime = d1t;

                    meterlist.Add(meterReadings);

                }
             
            }

                ;
                return meterlist;

            }
        }
    }

