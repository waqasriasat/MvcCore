﻿using System.ComponentModel.DataAnnotations;

namespace AdvLab_WebApp.Models
{
    public class AddDiscountAuthority
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DiscountAllow { get; set; }
    }
}
