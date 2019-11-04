using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Countries
    {
        public Countries()
        {
            States = new HashSet<States>();
        }

        public int Id { get; set; }
        public string Sortname { get; set; }
        public string Name { get; set; }
        public int Phonecode { get; set; }

        public ICollection<States> States { get; set; }
    }
}
