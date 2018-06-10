namespace SamuraiModel
{
    public partial class SamuraiBattle
    {
        public int SamuraiId { get; set; }
        public int BattleId { get; set; }

        public Battle Battle { get; set; }
        public Samurai Samurai { get; set; }
    }
}
