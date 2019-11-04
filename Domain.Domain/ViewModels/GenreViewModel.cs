using System.Collections.Generic;

namespace ELI.Domain.ViewModels
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        public IList<TrackViewModel> Tracks { get; set; }
    }
}
