﻿using System.ComponentModel.DataAnnotations;

namespace AdvLab_WebApp.Models
{
    public class AddBalanceAuthority
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BalanceAllow { get; set; }
    }
}
