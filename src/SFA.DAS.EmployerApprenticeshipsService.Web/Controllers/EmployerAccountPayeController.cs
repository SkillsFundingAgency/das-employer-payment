﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SFA.DAS.EmployerApprenticeshipsService.Web.Authentication;
using SFA.DAS.EmployerApprenticeshipsService.Web.Models;
using SFA.DAS.EmployerApprenticeshipsService.Web.Orchestrators;

namespace SFA.DAS.EmployerApprenticeshipsService.Web.Controllers
{
    public class EmployerAccountPayeController : Controller
    {
        private readonly IOwinWrapper _owinWrapper;
        private readonly EmployerAccountPayeOrchestrator _employerAccountPayeOrchestrator;

        public EmployerAccountPayeController(IOwinWrapper owinWrapper, EmployerAccountPayeOrchestrator employerAccountPayeOrchestrator)
        {
            if (owinWrapper == null)
                throw new ArgumentNullException(nameof(owinWrapper));
            if (employerAccountPayeOrchestrator == null)
                throw new ArgumentNullException(nameof(employerAccountPayeOrchestrator));
            _owinWrapper = owinWrapper;
            _employerAccountPayeOrchestrator = employerAccountPayeOrchestrator;
        }

        [HttpGet]
        public async Task<ActionResult> Index(long accountid)
        {
            var userIdClaim = _owinWrapper.GetClaimValue(@"sub");
            if (string.IsNullOrWhiteSpace(userIdClaim)) return RedirectToAction("Index", "Home");

            var schemes = await _employerAccountPayeOrchestrator.Get(accountid, userIdClaim);

            return View(new EmployerAccountPayeListViewModel
            {
                AccountId = accountid,
                PayeSchemes = schemes
            });
        }

        [HttpGet]
        public async Task<ActionResult> Details(long accountId, string empRef)
        {
            var userIdClaim = _owinWrapper.GetClaimValue(@"sub");
            if (string.IsNullOrWhiteSpace(userIdClaim)) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Add(long accountId)
        {
            var userIdClaim = _owinWrapper.GetClaimValue(@"sub");
            if (string.IsNullOrWhiteSpace(userIdClaim)) return RedirectToAction("Index", "Home");

            return View();
        }

    }
}