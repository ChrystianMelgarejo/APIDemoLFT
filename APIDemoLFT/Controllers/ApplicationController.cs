using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemoLFT.DataAccess;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace APIDemoLFT.Controllers
{
    public class ApplicationController : Controller
    {
        private LFTDatabase _dbAccess;
        public ApplicationController(LFTDatabase dbAccess)
        {
            _dbAccess = dbAccess;
        }        
        public IActionResult Index()
        {
            return View();
        }

        #region Get Methods

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetApplications()
        {
            return Ok(_dbAccess.GetApplications());
        }

        [HttpGet]
        [Route("api/GetApplication")]
        public IActionResult GetApplication(int applicationId)
        {
            return Ok(_dbAccess.GetApplication(applicationId));
        }

        [HttpPost]
        [Route("api/SearchApplications")]
        public IActionResult SearchApplications([FromBody] Search searchCriteria)
        {
            return Ok(_dbAccess.SearchApplications(searchCriteria));
        }

        #endregion Get Methods

        #region Create Methods

        [HttpGet]
        [Route("api/CreateApplication")]
        public IActionResult CreateApplication()
        {   
            return Ok(_dbAccess.CreateApplication());
        }

        #endregion Create Methods

        #region Edit Methods

        [HttpPost]
        [Route("api/EditApplication")]
        public IActionResult EditApplication([FromBody] Application applicationToUpdate)
        {
            _dbAccess.EditApplication(applicationToUpdate);
            return Ok();
        }

        #endregion Edit Methods

        #region Delete Methods

        [Route("api/DeleteApplication")]
        public IActionResult DeleteApplication(int applicationId)
        {
            _dbAccess.DeleteApplication(applicationId);
            return Ok();
        }

        #endregion Delete Methods
    }
}