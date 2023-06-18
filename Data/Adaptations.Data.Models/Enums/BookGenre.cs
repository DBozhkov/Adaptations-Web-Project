using System.Runtime.Serialization;

namespace Adaptations.Data.Models.Enums
{
    public enum BookGenre
    {
        Fantasy = 1,
        [EnumMember(Value = "Science Fiction")]
        ScienceFiction = 2,
        Mystery = 3,
        Romance = 4,
        Thriller = 5,
        Horror = 6,
        Biography = 7,
        Classic = 8,
        Poetry = 9,
    }
}
