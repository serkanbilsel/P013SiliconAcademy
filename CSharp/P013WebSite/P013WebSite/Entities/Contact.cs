﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace P013WebSite.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        [Display(Name = "Ad "), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Soyad"), StringLength(50)]
        public string SurName { get; set; }
        [StringLength(50)]

        public string Email { get; set; }
        [Display(Name = "Telefon"), StringLength(15)]
        public string Phone { get; set; }
        [Display(Name = "Mesajınız")]
        public string Message { get; set; }
        [Display(Name = "Eklenme Tarihi"),]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
