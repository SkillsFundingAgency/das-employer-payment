﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SFA.DAS.EmployerApprenticeshipsService.Domain.Interfaces;
using SFA.DAS.EmployerApprenticeshipsService.Web.Authentication;
using SFA.DAS.EmployerApprenticeshipsService.Web.Orchestrators;

namespace SFA.DAS.EmployerApprenticeshipsService.Web.Controllers
{
    [Authorize]
    [RoutePrefix("accounts/{hashedaccountId}")]
    public class EmployerTasksController : BaseController
    {
        private readonly IOwinWrapper _owinWrapper;
        private readonly EmployerTasksOrchestrator _employerTasksOrchestrator;

        public EmployerTasksController(IOwinWrapper owinWrapper, EmployerTasksOrchestrator employerTasksOrchestrator, IFeatureToggle featureToggle, IUserWhiteList userWhiteList) : base(owinWrapper, featureToggle, userWhiteList)
        {
            if (owinWrapper == null)
                throw new ArgumentNullException(nameof(owinWrapper));
            if (employerTasksOrchestrator == null)
                throw new ArgumentNullException(nameof(employerTasksOrchestrator));
            _owinWrapper = owinWrapper;
            _employerTasksOrchestrator = employerTasksOrchestrator;
        }

        [HttpGet]
        [Route("Tasks/List")]
        public async Task<ActionResult> Index(string hashedaccountId)
        {
            var userIdClaim = _owinWrapper.GetClaimValue(@"sub");

            var response = await _employerTasksOrchestrator.GetTasks(hashedaccountId, userIdClaim);

            return View(response);
        }

        [HttpGet]
        [Route("Tasks/{taskId}")]
        public async Task<ActionResult> View(string hashedaccountId, long taskId)
        {
            var userIdClaim = _owinWrapper.GetClaimValue(@"sub");

            var response = await _employerTasksOrchestrator.GetTask(hashedaccountId, taskId, userIdClaim);

            return View(response);
        }
    }
}
