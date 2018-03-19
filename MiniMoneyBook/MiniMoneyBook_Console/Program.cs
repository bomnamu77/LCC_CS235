// Demo of using SQLite-net ORM
// Brian Bird, 5/20/13
// Converted to an exercise starter and completed
// By Brian Bird 5/12/16
// Modified by Soyoung Kim 3/14/18

using System;
using SQLite;
using System.IO;
using MiniMoneyBook.DAL;
using System.Collections.Generic;
using static Android.InputMethodServices.InputMethodService;


namespace MiniMoneyBook_Console
{
    class MainClass 
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello SQLite-net Data!");

            // We're using a file in Assets instead of the one defined above
            //string dbPath = Directory.GetCurrentDirectory ();
            //string dbPath = @"Tides.db3";
            string dbPath = @"../../../MiniMoneyBook/Assets/MoneyBook.db3";
            var db = new SQLiteConnection(dbPath);

            // Create a Stocks table
            //if (db.CreateTable<MoneyBook>() == 0)
            {
                // A table already exixts, delete any data it contains
                //db.DeleteAll<Tide>();
            }


            InitDb(db);        

        }
        private static void InitDb(SQLiteConnection db)
        {

            // Create tables
            db.CreateTable<MoneyBook>();
            //if Category is created newly, add category data
            if (db.CreateTable<Category>() == 0)
            {
                db.Insert(new Category()
                { I_E = "I", CategoryName = "Salary" });
                db.Insert(new Category()
                { I_E = "I", CategoryName = "Bonus" });
                db.Insert(new Category()
                { I_E = "I", CategoryName = "Others" });

                db.Insert(new Category()
                { I_E = "E", CategoryName = "Food" });
                db.Insert(new Category()
                { I_E = "E", CategoryName = "Bills" });
                db.Insert(new Category()
                { I_E = "E", CategoryName = "Personals" });

                db.Insert(new Category()
                { I_E = "E", CategoryName = "Others" });


            }
        }

    }
}
