﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFriendsDB.Models.ConnectionModels
{
    public class ConnectionListItem
    {
        [Key]
        [Display(Name = "Connections ID")]
        public int ConnectionsId { get; set; }

        [Display(Name = "Character ID")]
        public int CharacterId { get; set; }
        
        [Required]
        public string GroupAffiliation { get; set; }

        [Required]
        public string Relatives { get; set; }
    }
}
