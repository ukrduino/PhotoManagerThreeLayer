﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManagerModels.Models.Interfaces;

namespace PhotoManagerModels.Models
{
    public class Album: IHasLastModifiedField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; }
        public byte[] CoverImageData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }

        public Album()
        {
            CreatedDate = DateTime.Now;
        }
    }
}