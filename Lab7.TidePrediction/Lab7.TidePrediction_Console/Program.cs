// Demo of using SQLite-net ORM
// Brian Bird, 5/20/13
// Converted to an exercise starter and completed
// By Brian Bird 5/12/16

using System;
using SQLite;
using System.IO;
using Lab7.TidePrediction.DAL;
using System.Collections.Generic;

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

            AddTidesToDb(db, "Astoria", "Astoria.csv");
            AddTidesToDb(db, "CoosBay", "CoosBay.csv");
            AddTidesToDb(db, "Florence", "Florence.csv");
        }

        private static void AddTidesToDb(SQLiteConnection db, string location, string file)
        {
            // parse the stock csv file
            const int NUMBER_OF_FIELDS = 6;    // The text file will have 6 fields, The first is the date, the last is the High/Low
            TextParser parser = new TextParser(",", NUMBER_OF_FIELDS);     // instantiate our general purpose text file parser object.
            List<string[]> stringArrays;    // The parser generates a List of string arrays. Each array represents one line of the text file.
            stringArrays = parser.ParseText(File.Open(@"../../../Lab7.TidePrediction_Console/DAL/" + file, FileMode.Open));     // Open the file as a stream and parse all the text

            // Don't use the first array, it's a header
            stringArrays.RemoveAt(0);

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
                    
                    DateTimes = Convert.ToDateTime(TideInfo[0] + " " + TideInfo[2]),
                    Day = Convert.ToString(TideInfo[1]),
                    //Time = Convert.ToDateTime(TideInfo[2]),
                    Height = decimal.Parse(TideInfo[3]),
                    H_L= Convert.ToString(TideInfo[5])
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
