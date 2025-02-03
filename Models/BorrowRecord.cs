﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library_Management_System.Models
{
    public class BorrowRecord
    {
        [Key]
        public int BorrowRecordId { get; set; }
        [Required]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter borrwer name")]
        public string BorrowerName { get; set; }

        [Required(ErrorMessage = "Please enter borrwer email address")]
        [EmailAddress(ErrorMessage = "Please enter a Email Address")]
        public string BorrowerEmail { get; set; }

        [Required(ErrorMessage = "Please enter Borrower Phone Number")]
        [Phone(ErrorMessage = "Please enter a Valid Phone Number")]
        public string Phone {  get; set; }

        [BindNever]
        [DataType(DataType.DateTime)]
        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }

        [BindNever]
        public Book Book {  get; set; }

    }
}
