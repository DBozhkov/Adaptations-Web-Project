﻿namespace Adaptations.Data.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string CharacterName { get; set; }

        public string CharacterDescription { get; set; }

        public virtual Book Book { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
