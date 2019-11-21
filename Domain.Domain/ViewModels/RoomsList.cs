using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domain.ViewModels
{
    public class RoomsList
    {
        public int ID { get; set; }
        public string RoomID { get; set; }
        public string Campus { get; set; }
        public string Building { get; set; }
        public string RoomType { get; set; }
        public string Floor { get; set; }
        public string Ldx { get; set; }
        public string Notes { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public bool Available { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public int ImportedOne { get; set; }
        public string Weekno { get; set; }
        public int Year { get; set; }
        public bool Active { get; set; }
    }
}
