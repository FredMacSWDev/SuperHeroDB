﻿using Newtonsoft.Json;
using SuperFriendsDB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFriendsDB.Models.PowerstatModels
{
    public class PowerstatListItem
    {
        [Key]
        public int StatId { get; set; }
        [ForeignKey("CharacterId")]
        public virtual List<Character> Character { get; set; }

        [ForeignKey("HeroName")]
        public string Name { get; }

        [Required]
        [JsonProperty("intelligence")]
        public string Intelligence { get; set; }

        [Required]
        [JsonProperty("strength")]
        public string Strength { get; set; }

        [Required]
        [JsonProperty("speed")]
        public string Speed { get; set; }

        [Required]
        [JsonProperty("durability")]
        public string Durability { get; set; }

        [Required]
        [JsonProperty("power")]
        public string Power { get; set; }

        [Required]
        [JsonProperty("combat")]
        public string Combat { get; set; }
    }
}
