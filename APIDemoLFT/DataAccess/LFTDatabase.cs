using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIDemoLFT.DataAccess
{
    public class LFTDatabase
    {
        private LFTDatabaseContext _db;
        
        public LFTDatabase(LFTDatabaseContext db)
        {
            _db = db;

            if (_db.Clients.Count() < 1)
            {
                string file = System.IO.File.ReadAllText("C:\\Users\\Chrystian\\source\\repos\\APIDemoLFT\\APIDemoLFT\\Seed Data\\Clients.json");
                var clients = JsonSerializer.Deserialize<List<Client>>(file);
                _db.Clients.AddRange(clients);
                _db.SaveChanges();
            }

            if (_db.Businesses.Count() < 1)
            {
                string file = System.IO.File.ReadAllText("C:\\Users\\Chrystian\\source\\repos\\APIDemoLFT\\APIDemoLFT\\Seed Data\\Business.json");
                var businesses = JsonSerializer.Deserialize<List<Business>>(file);
                _db.Businesses.AddRange(businesses);
                _db.SaveChanges();
            }

            if (_db.Loans.Count() < 1)
            {
                string file = System.IO.File.ReadAllText("C:\\Users\\Chrystian\\source\\repos\\APIDemoLFT\\APIDemoLFT\\Seed Data\\Loans.json");
                var loans = JsonSerializer.Deserialize<List<Loan>>(file);
                _db.Loans.AddRange(loans);
                _db.SaveChanges();
            }

            if (_db.Applications.Count() < 1)
            {
                string file = System.IO.File.ReadAllText("C:\\Users\\Chrystian\\source\\repos\\APIDemoLFT\\APIDemoLFT\\Seed Data\\Application.json");
                var applications = JsonSerializer.Deserialize<List<Application>>(file);
                _db.Applications.AddRange(applications);
                _db.SaveChanges();
            }
        }

        public List<Application> GetApplications()
        {
            List<Application> applications = _db.Applications.Where(a => a.IsDeleted == false).OrderBy(a => a.ApplicationId).ToList();

            foreach (var application in applications)
            {
                if(application.ClientId != null)
                    application.Client = _db.Clients.Where(c => c.ClientId == application.ClientId).Single();
                else
                    application.Client = new Client();

                if (application.BusinessId != null)
                    application.Business = _db.Businesses.Where(b => b.BusinessId == application.BusinessId).Single();
                else
                    application.Business = new Business();

                if (application.LoanId != null)
                    application.Loan = _db.Loans.Where(l => l.LoanId == application.LoanId).Single();
                else
                    application.Loan = new Loan();
            }

            return applications;
        }

        public Application GetApplication(int applicationId)
        {
            Application application = _db.Applications.Where(a => a.IsDeleted == false && a.ApplicationId == applicationId).SingleOrDefault();

            if(application != null)
            {
                if (application.ClientId != null)
                    application.Client = _db.Clients.Where(c => c.ClientId == application.ClientId).Single();
                else
                    application.Client = new Client();

                if (application.BusinessId != null)
                    application.Business = _db.Businesses.Where(b => b.BusinessId == application.BusinessId).Single();
                else
                    application.Business = new Business();

                if (application.LoanId != null)
                    application.Loan = _db.Loans.Where(l => l.LoanId == application.LoanId).Single();
                else
                    application.Loan = new Loan();
            }

            return application;
        }

        public void DeleteApplication(int applicationId)
        {
            Application mySelectedApplication = _db.Applications.Where(a => a.ApplicationId == applicationId).Single();
            mySelectedApplication.IsDeleted = true;
            _db.SaveChanges();
        }

        public void EditApplication(Application applicationToUpdate)
        {
            if (applicationToUpdate.Status == 1 && applicationToUpdate.ClientId == null && applicationToUpdate.BusinessId == null && applicationToUpdate.LoanId == null) 
            {
                Random r = new Random();
                applicationToUpdate.Loan.CreditScore = r.Next(600, 750);
                applicationToUpdate.Loan.APRRate = r.Next(4, 12);
                applicationToUpdate.Status = 2;
                
                _db.Clients.Add(applicationToUpdate.Client);
                _db.Businesses.Add(applicationToUpdate.Business);
                _db.Loans.Add(applicationToUpdate.Loan);
                _db.Applications.Update(applicationToUpdate);
                _db.SaveChanges();
            }
            else {

                decimal calculatedRisk = CalculateRiskAssessment(ref applicationToUpdate);

                applicationToUpdate.Loan.RiskAssessment = calculatedRisk;
                applicationToUpdate.Status = ((calculatedRisk >= 8.0M) ? 3 : 4); //[3: Approved] [4: Rejected]

                _db.Clients.Update(applicationToUpdate.Client);
                _db.Businesses.Update(applicationToUpdate.Business);
                _db.Loans.Update(applicationToUpdate.Loan);
                _db.Applications.Update(applicationToUpdate);
                _db.SaveChanges();
            }
        }

        public List<Application> SearchApplications(Search searchCriteria)
        {
            List<Application> matchedApplications = new List<Application>();

            matchedApplications = (from a in _db.Applications
                                   where a.IsDeleted == false &&
                                   a.ApplicationId == (searchCriteria.ApplicationId == null ? a.ApplicationId : searchCriteria.ApplicationId) && 
                                   a.Status == (searchCriteria.Status == null ? a.Status : searchCriteria.Status) &&
                                   a.Client.FirstName == (searchCriteria.ClientFirstName == null ? a.Client.FirstName : searchCriteria.ClientFirstName) &&
                                   a.Client.LastName == (searchCriteria.ClientLastName == null ? a.Client.LastName : searchCriteria.ClientLastName) &&
                                   a.Client.State == (searchCriteria.State == null ? a.Client.State : searchCriteria.State) &&
                                   a.Business.Name == (searchCriteria.BusinessName == null ? a.Business.Name : searchCriteria.BusinessName) &&
                                   a.Loan.CreditScore == (searchCriteria.CreditScore == null ? a.Loan.CreditScore : searchCriteria.CreditScore) &&
                                   a.Loan.LoanAmount >= (searchCriteria.MinimumLoanAmoun == null ? a.Loan.LoanAmount : searchCriteria.MinimumLoanAmoun) &&
                                   a.Loan.LoanAmount <= (searchCriteria.MaximumLoanAmoun == null ? a.Loan.LoanAmount : searchCriteria.MaximumLoanAmoun)
                                   select a).ToList();

            foreach (var application in matchedApplications)
            {
                if (application.ClientId != null)
                    application.Client = _db.Clients.Where(c => c.ClientId == application.ClientId).Single();

                if (application.BusinessId != null)
                    application.Business = _db.Businesses.Where(b => b.BusinessId == application.BusinessId).Single();

                if (application.LoanId != null)
                    application.Loan = _db.Loans.Where(l => l.LoanId == application.LoanId).Single();
            }

            return matchedApplications;
        }

        public Application CreateApplication()
        {
            Application newApplication = new Application();
            newApplication.IsDeleted = false;
            newApplication.Status = 1;

            _db.Applications.Add(newApplication);
            _db.SaveChanges();

            newApplication.Client = new Client();
            newApplication.Business = new Business();
            newApplication.Loan = new Loan();            

            return newApplication;
        }

        private decimal CalculateRiskAssessment(ref Application applicationToUpdate)
        {
            decimal calculateRisk = 0;
            
            int collegeDegreeValue = 0;
            int employmentStatusValue = 0;
            int isUnderAnotherLoanValue = 0;
            int estimatedGrossAnnualRevenue = 0;
            int monthsToPayBackValue = 0;
            int aprRateValue = (applicationToUpdate.Loan.APRRate >= 4 && applicationToUpdate.Loan.APRRate <= 8) ? 9 : 10;
            int creditScoreValue = (applicationToUpdate.Loan.CreditScore <= 700) ? 8 : 10;
            int outstandingDebt = (applicationToUpdate.Loan.CreditScore <= 50000) ? 10 : 8;

            switch (applicationToUpdate.Client.CollegeDegree)
            {
                case "Less than high school degree":
                    collegeDegreeValue = 3;
                    break;
                case "High school degree or equivalen":
                    collegeDegreeValue = 5;
                    break;
                case "Some college but no degree":
                    collegeDegreeValue = 6;
                    break;
                case "Associate degree":
                    collegeDegreeValue = 8;
                    break;
                case "Bachelor degree":
                    collegeDegreeValue = 10;
                    break;
                case "Graduate degree":
                    collegeDegreeValue = 9;
                    break;
            };

            switch (applicationToUpdate.Client.EmploymentStatus)
            {
                case "Employed":
                    employmentStatusValue = 10;
                    break;
                case "Not employed":
                    employmentStatusValue = 5;
                    break;
                case "Retired":
                    employmentStatusValue = 7;
                    break;
                case "Disabled, not able to work":
                    employmentStatusValue = 6;
                    break;
            };

            switch (applicationToUpdate.Business.IsUnderAnotherLoan)
            {
                case true:
                    isUnderAnotherLoanValue = 7;
                    break;
                case false:
                    isUnderAnotherLoanValue = 10;
                    break;
            };

            switch (applicationToUpdate.Loan.MonthsToPayBack)
            {
                case 12:
                    monthsToPayBackValue = 9;
                    break;
                case 24:
                    monthsToPayBackValue = 10;
                    break;
                case 36:
                    monthsToPayBackValue = 10;
                    break;
                case 48:
                    monthsToPayBackValue = 9;
                    break;
                case 60:
                    monthsToPayBackValue = 8;
                    break;
            };

            if (applicationToUpdate.Business.EstimatedGrossAnnualRevenue <= 50000)
                estimatedGrossAnnualRevenue = 6;
            else if (applicationToUpdate.Business.EstimatedGrossAnnualRevenue > 50000 && applicationToUpdate.Business.EstimatedGrossAnnualRevenue <= 90000)
                estimatedGrossAnnualRevenue = 8;
            else if (applicationToUpdate.Business.EstimatedGrossAnnualRevenue > 90000)
                estimatedGrossAnnualRevenue = 10;


            calculateRisk = (collegeDegreeValue * 0.125M) + (employmentStatusValue * 0.125M) + (isUnderAnotherLoanValue * 0.125M) + (estimatedGrossAnnualRevenue * 0.125M) +
                            (monthsToPayBackValue * 0.125M) + (aprRateValue * 0.125M) + (creditScoreValue * 0.125M) + (outstandingDebt * 0.125M);

            return calculateRisk;
        }
    }
}
