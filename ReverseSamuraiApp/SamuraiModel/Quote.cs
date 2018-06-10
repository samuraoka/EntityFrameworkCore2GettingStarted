namespace SamuraiModel
{
    public partial class Quote
    {
        public int Id { get; set; }
        public int SamuraiId { get; set; }
        public string Text { get; set; }

        public Samurai Samurai { get; set; }
    }
}
