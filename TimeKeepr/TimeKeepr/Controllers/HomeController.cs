﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeepr.Models;

namespace TimeKeepr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        TimeTracking timeTracker = new TimeTracking();
       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Start()
        {
            timeTracker.Start();
            var (startTime, isStart, isEnd) = timeTracker.Start();
            ViewBag.Start = isStart;
            ViewBag.End = isEnd;
            ViewBag.StartTime = startTime;
            return View("Index");
        }

        public IActionResult End()
        {

            //var user = User.Identity.Name;
            //var permission = User.IsInRole("admin");

            try
            {
                var (startTime, endTime, started, ended,
                currentTotal, total) = timeTracker.End();
                ViewBag.StartTime = startTime;
                ViewBag.EndTime = endTime;
                ViewBag.Current = currentTotal;
                ViewBag.Total = total;
                ViewBag.Start = started;
                ViewBag.End = ended;
            }
            catch(Exception)
            {
            }

            return View("Index");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
