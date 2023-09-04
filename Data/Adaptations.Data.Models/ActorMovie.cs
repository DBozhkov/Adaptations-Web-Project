namespace Adaptations.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ActorMovie
    {
        [ForeignKey(nameof(Actor))]
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
