using InfoTracker.MockData.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace InfoTracker.UnitTest.Repository
{
    public class PlayListTrackRepositoryTest
    {
        private readonly AlbumRepository _repo;

        public PlayListTrackRepositoryTest()
        {
            _repo = new AlbumRepository();
        }

        [Fact]
        public async Task PlayListTrackGetAllAsync()
        {
            // Act
            var playListTracks = await _repo.GetAllAsync();

            // Assert
            Assert.Equal(1, playListTracks.Count);
        }
    }
}
