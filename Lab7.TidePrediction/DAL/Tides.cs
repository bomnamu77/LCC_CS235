﻿using System;
using SQLite;

namespace Lab7.TidePrediction.DAL
{
    [Table("Tides")]
    public class Tide
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(8)]
       
        public string Location { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public decimal Height { get; set; }
        public string H_L { get; set; }
    }
}