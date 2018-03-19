using System;
using SQLite;

namespace MiniMoneyBook.DAL
{
    [Table("MoneyBook")]
    public class MoneyBook
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(8)]
       
        public string I_E { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Memo { get; set; }
        
    }

    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(8)]

        public string I_E { get; set; }
        public string CategoryName { get; set; }
        

    }
}