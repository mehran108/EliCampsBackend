using System.Collections.Generic;

namespace InfoTracker.MockData.DataModels
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
    }
}
