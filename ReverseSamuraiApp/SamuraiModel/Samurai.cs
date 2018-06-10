using System.Collections.Generic;

namespace SamuraiModel
{
    public partial class Samurai
    {
        public Samurai()
        {
            Quote = new HashSet<Quote>();
            SamuraiBattle = new HashSet<SamuraiBattle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public SecretIdentity SecretIdentity { get; set; }
        public ICollection<Quote> Quote { get; set; }
        public ICollection<SamuraiBattle> SamuraiBattle { get; set; }
    }
}
