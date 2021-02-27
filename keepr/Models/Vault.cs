namespace keepr.Models
{
    public class Vault
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; } = false;
        public Profile Creator { get; set; }

        public Vault()
        {

        }
    }

}