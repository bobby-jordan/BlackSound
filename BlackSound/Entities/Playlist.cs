using System.Collections.Generic;

namespace BlackSound.Entities
{
    public class Playlist : IEntity
    {
        public int Id { get; set; }
        public int UserOwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Songs { get; set; }
        public bool IsPublic { get; set; }

        public List<int> SongsIts { get; set; }
    }
}
