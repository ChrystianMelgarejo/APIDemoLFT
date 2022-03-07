using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary
{
    public class Business
	{
		[Key]
		[Required]
		public int BusinessId { get; set; }

		[Required(ErrorMessage = "Name is a required text field. Min Length = 2, Max Length = 250")]
		[Column(TypeName = "varchar(250)")]
		[MinLength(2, ErrorMessage = "Name is a required text field. Min Length = 2, Max Length = 250")]
		[MaxLength(250, ErrorMessage = "Name is a required text field. Min Length = 2, Max Length = 250")]
		public string Name { get; set; }

		
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

		
		
		[Required(ErrorMessage = "Started Year is a required text field.")]
		[Column(TypeName = "date")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime StartedYear { get; set; }

		
		
		[Required(ErrorMessage = "Is Under Another Loan? is a required text field.")]
		public bool IsUnderAnotherLoan { get; set; }



		[Required(ErrorMessage = "Business Tax Id is a required text field. Min Length = 9, Max Length = 9")]
		[Column(TypeName = "varchar(9)")]
		[MinLength(9, ErrorMessage = "Business Tax Id is a required text field. Min Length = 9, Max Length = 9")]
		[MaxLength(9, ErrorMessage = "Business Tax Id is a required text field. Min Length = 9, Max Length = 9")]
		public string BusinessTaxId { get; set; }
		
		
		
		[Required(ErrorMessage = "Estimated Gross Annual Revenue is a required text field.")]
		public int EstimatedGrossAnnualRevenue { get; set; }
	}
}
