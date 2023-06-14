using System.Runtime.Serialization;

namespace Adaptations.Data.Models.Enums
{
    public enum MovieGenre
    {
        Action = 1,
        Comedy = 2,
        Drama = 3,
        [EnumMember(Value = "Sci-Fi")]
        Sci_Fi = 4,
        Fantasy = 5,
        Horror = 6,
        Romance = 7,
        Thriller = 8,
        Animation = 9,
        Adventure = 10,
    }
}
