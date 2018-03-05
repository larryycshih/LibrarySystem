using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class Library
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public MediaTypes? MediaType { get; set; }

        [Required]
        public MediaLanguage? MediaLanguage { get; set; }

        [Required]
        public MediaCategory? MediaCategory { get; set; }
        
        public String ReferenceID { get; set; }

        [Required]
        public String Name { get; set; }

        public String Author { get; set; }

        public String Publisher { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePurchased { get; set; }

        [DataType(DataType.Currency)]
        public Double Price { get; set; }

        [Display(Name="ISBN/ISSN")]
        public String ISBN_ISSN { get; set; }

        public String Comment { get; set; }
    }

    public enum MediaTypes
    {
        Book,CD,VCD,DVD,Journals,MP3,SongBooks,Tape,Video,Others

    }
    public enum MediaLanguage
    {
        Cantonese,English,Mandarin,Taiwaness,Others

    }
    public enum MediaCategory
    {
       解經,靈修,神學,屬靈,禱告,心靈,生活,故事,其他種類

    }
}