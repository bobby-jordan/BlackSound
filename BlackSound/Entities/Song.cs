
namespace BlackSound.Entities
{
    public class Song:IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public int Year { get; set; }
    }
}
