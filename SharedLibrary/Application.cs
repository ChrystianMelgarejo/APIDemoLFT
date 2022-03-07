using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary
{
    public class Application
	{		
		[Key]
		[Required]
		public int ApplicationId { get; set; }
		public int? ClientId { get; set; }
		public int? BusinessId { get; set; }
		public int? LoanId { get; set; }
		
		[Required]
		public int Status { get; set; }
		
		[DefaultValue("false")]
		public bool IsDeleted { get; set; }
		public Client Client { get; set; }
		public Business Business { get; set; }
		public Loan Loan { get; set; }
	}
}