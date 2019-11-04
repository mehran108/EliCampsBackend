using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Server
    {
        public Server()
        {
            Database = new HashSet<Database>();
        }

        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public string ServerUrl { get; set; }

        public ICollection<Database> Database { get; set; }
    }
}
