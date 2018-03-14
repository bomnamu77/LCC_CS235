// Demo of using SQLite-net ORM
// Brian Bird, 5/20/13
// Converted to an exercise starter and completed
// By Brian Bird 5/12/16
// Modified by Soyoung Kim 3/14/18

using System;
using SQLite;
using System.IO;
using Lab7.TidePrediction.DAL;
using System.Collections.Generic;
using static Android.InputMethodServices.InputMethodService;


namespace Lab7.TidePrediction_Console
{
    class MainClass 
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello SQLite-net Data!");

            // We're using a file in Assets instead of the one defined above
            //string dbPath = Directory.GetCurrentDirectory ();
            //string dbPath = @"Tides.db3";
            string dbPath = @"../../../Lab7.TidePrediction/Assets/Tides.db3";
            var db = new SQLiteConnection(dbPath);

            // Create a Stocks table
            if (db.CreateTable<Tide>() == 0)
            {
                // A table already exixts, delete any data it contains
                db.DeleteAll<Tide>();
            }

            AddTidesToDb(db, "Astoria", "Astoria.xml");
            AddTidesToDb(db, "CoosBay", "CoosBay.xml");
            AddTidesToDb(db, "Florence", "Florence.xml");
        }

        private static void AddTidesToDb(SQLiteConnection db, string location, string file)
        {
            List<string[]> stringArrays;

            // Read and prase the vocabulary file and provide the adapter with the data
            var reader = new XmlTideFileParser(File.Open(@"../../../Lab7.TidePrediction_Console/DAL/" + file, FileMode.Open));
            stringArrays = reader.TideList;

            // Show the first date in Ticks
            DateTime firstDate = Convert.ToDateTime(stringArrays[0][0]);
            Console.WriteLine("Beginning Date: {0} = {1} Ticks", firstDate.ToString(), firstDate.Ticks);

            // Copy the List of strings into our Database
            int pk = 0;
            foreach (string[] TideInfo in stringArrays)
            {
                pk += db.Insert(new Tide()
                {
                    Location = location,

                    Date = Convert.ToString(TideInfo[0]),
                    Day = Convert.ToString(TideInfo[1]),
                    Time = Convert.ToString(TideInfo[2]),
                    Height = decimal.Parse(TideInfo[3]),
                    H_L = Convert.ToString(TideInfo[4])
                });
                // Give an update every 100 rows
                if (pk % 100 == 0)
                    Console.WriteLine("{0} {1} rows inserted", pk, location);
            }
            // Show the final count of rows inserted
            Console.WriteLine("{0} {1} rows inserted", pk, location);

        

        }
        
    }
}
