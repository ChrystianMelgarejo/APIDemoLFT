using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary
{
    public class Client
	{
		[Key]
		[Required]
		public int ClientId { get; set; }

		[Required(ErrorMessage = "First Name is a required text field. Min Length = 2, Max Length = 250")]
		[Column(TypeName = "varchar(250)")]
		[MinLength(2, ErrorMessage = "First Name is a required text field. Min Length = 2, Max Length = 250")]
		[MaxLength(250, ErrorMessage = "First Name is a required text field. Min Length = 2, Max Length = 250")]		
		public string FirstName { get; set; }
		
		
		[Required(ErrorMessage = "Last Name is a required text field. Min Length = 2, Max Length = 250")]
		[Column(TypeName = "varchar(250)")]
		[MinLength(2, ErrorMessage = "Last Name is a required text field. Min Length = 2, Max Length = 250")]
		[MaxLength(250, ErrorMessage = "Last Name is a required text field. Min Length = 2, Max Length = 250")]
		public string LastName { get; set; }

		
		[Required(ErrorMessage = "Date Of Birth is required")]
		[Column(TypeName = "date")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime DateOfBirth { get; set; }

		
		[Required(ErrorMessage = "Street Address is a required text field. Min Length = 10, Max Length = 500")]
		[Column(TypeName = "varchar(500)")]
		[MinLength(10, ErrorMessage = "Street Address is a required text field. Min Length = 10, Max Length = 500")]
		[MaxLength(500, ErrorMessage = "Street Address is a required text field. Min Length = 10, Max Length = 500")]
		public string StreetAddress { get; set; }

		
		[Required(ErrorMessage = "City is a required text field. Min Length = 2, Max Length = 100")]
		[Column(TypeName = "varchar(100)")]
		[MinLength(2, ErrorMessage = "City is a required text field. Min Length = 2, Max Length = 100")]
		[MaxLength(100, ErrorMessage = "City is a required text field. Min Length = 2, Max Length = 100")]
		public string City { get; set; }



		[Required(ErrorMessage = "State is a required text field.")]
		[Column(TypeName = "varchar(2)")]		
		public string State { get; set; }



		[Required(ErrorMessage = "Zip Code is a required text field. Min Length = 5, Max Length = 10")]
		[Column(TypeName = "varchar(10)")]
		[MinLength(5, ErrorMessage = "Zip Code is a required text field. Min Length = 5, Max Length = 10")]
		[MaxLength(10, ErrorMessage = "Zip Code is a required text field. Min Length = 5, Max Length = 10")]
		public string ZipCode { get; set; }		
		
		
		[Column(TypeName = "varchar(250)")]		
		public string Gender { get; set; }

		
		[Column(TypeName = "varchar(250)")]
		public string MaritalStatus { get; set; }		
		

		
		[Column(TypeName = "varchar(250)")]		
		public string Race { get; set; }


		
		[Required(ErrorMessage = "College Degree is a required text field.")]
		[Column(TypeName = "varchar(250)")]
		public string CollegeDegree { get; set; }


		[Required(ErrorMessage = "Employment Status is a required text field.")]
		[Column(TypeName = "varchar(250)")]
		public string EmploymentStatus { get; set; }
	}
}
