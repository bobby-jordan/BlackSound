
namespace BlackSound.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{this.Username} ({this.Email})";
        }
    }
}
