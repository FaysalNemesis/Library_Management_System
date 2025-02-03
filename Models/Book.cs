﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library_Management_System.Models
{
    public class Book
    {
        [BindNever]
        public int BookId { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Author field is required.")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The ISBN field is required.")]
        [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "ISBN must be in the format XXX-XXXXXXXXXX.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "The Published Date field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        [BindNever]
        [Display(Name = "Availabe")]
        public bool IsAvailable { get; set; }

        [BindNever]
        public ICollection <BorrowRecord>? BorrowRecords { get; set; }


    }
}
