using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary
{
    public class Search
	{	
		public int? ApplicationId { get; set; }
		public int? Status { get; set; }				
		public string ClientFirstName { get; set; }
		public string ClientLastName { get; set; }
		public string State { get; set; }
		public string BusinessName { get; set; }
		public int? CreditScore { get; set; }
		public int? MinimumLoanAmoun { get; set; }
		public int? MaximumLoanAmoun { get; set; }
	}
}