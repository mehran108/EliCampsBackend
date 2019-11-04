using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Database
    {
        public int DatabaseId { get; set; }
        public string DatabaseName { get; set; }
        public int? ServerId { get; set; }
        public string LeadsInfoURL { get; set; }

        public Server Server { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
