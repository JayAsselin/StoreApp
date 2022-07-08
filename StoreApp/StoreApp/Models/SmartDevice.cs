using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Models
{
    public class SmartDevice
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Modele { get; set; }
        public string Fabriquant { get; set; }
        public string Type { get; set; }
        public string PlateForme { get; set; }
        public double Prix { get; set; }
        public string Image { get; set; }

    }
}
