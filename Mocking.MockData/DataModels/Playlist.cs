using System.Collections.Generic;

namespace InfoTracker.MockData.DataModels
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new HashSet<PlaylistTrack>();
    }
}
