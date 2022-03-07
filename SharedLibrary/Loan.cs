using System;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary
{
    public class Loan
	{
		[Key]
		[Required]
		public int LoanId { get; set; }

		
		[Required(ErrorMessage = "Loan Amount is a required text field.")]
		public int LoanAmount { get; set; }


		[Required(ErrorMessage = "Months To Pay Back is a required text field.")] 
		public int MonthsToPayBack { get; set; }


		[Required(ErrorMessage = "APR Rate is a required text field.")]
		[Range(4, 12, ErrorMessage = "Enter number between 4 to 12")]
		public int APRRate { get; set; }


		[Required(ErrorMessage = "Credit Score is a required text field.")]
		[Range(600, 750, ErrorMessage = "Enter number between 600 to 750")]		 
		public int CreditScore { get; set; }


		[Required(ErrorMessage = "Outstanding Debt is a required text field.")]
		[Range(25000, 1000000, ErrorMessage = "Enter number between 25,000  to 1,000,000")]
		public int OutstandingDebt { get; set; }

		
		public decimal? RiskAssessment { get; set; }
	}
}
