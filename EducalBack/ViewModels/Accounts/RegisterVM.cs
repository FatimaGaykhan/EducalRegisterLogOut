﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EducalBack.ViewModels.Accounts
{
	public class RegisterVM
	{
		[Required]
		public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Email is Invalid")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


	}
}

