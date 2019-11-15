using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domain.ViewModels
{
    public class HomeStayViewModel
    {
        public int HomeId { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public string CellNumber{ get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string Intersection { get; set; }
        public string Distance { get; set; }
        public string Meals { get; set; }
        public string Prefer { get; set; }
        public string Rooms { get; set; }
        public string Aggrements { get; set; }
        public string PoliceCheck { get; set; }
        public bool Active { get; set; }
    }
}
