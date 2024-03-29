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

        bool started = false;

        bool ended = false;

        static Stopwatch stopwatch = new Stopwatch();

        static DateTime startTime;

        static DateTime endTime;

        static string stopwatchString;

        static string[] stopwatchList;

        double currentTotal;

        double total;

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
            stopwatch.Start();
            startTime = DateTime.Now;
            ViewBag.Start = true;
            started = true;
            ViewBag.End = false;
            ended = false;
            return View("Index");
        }

        public IActionResult End()
        {
            var user = User.Identity.Name;
            var permission = User.IsInRole("admin");

            if (started || ended == false && started == false)
            {
                stopwatch.Stop();

                endTime = DateTime.Now;
                ViewBag.StartTime = startTime;
                ViewBag.EndTime = endTime;
                var totalTime = stopwatch.Elapsed;
                stopwatchString = stopwatch.Elapsed.ToString();
                stopwatchList = stopwatchString.Split(":");
                var trimIndex = stopwatchList[2].IndexOf(".");
                var hours = Convert.ToInt32(stopwatchList[1]);
                var minutes = Convert.ToInt32(stopwatchList[2].Remove(trimIndex - 1, stopwatchList[2].Length - 1));


                if (hours > 0)
                {
                    currentTotal = Convert.ToInt32(hours) * 60 + Convert.ToInt32(minutes);
                }
                else if (minutes < 5)
                {
                    currentTotal = 5;
                }
                else
                {
                    currentTotal = Convert.ToInt32(stopwatchList[2]);
                }
                ViewBag.Current = currentTotal;
                this.total += currentTotal;
                ViewBag.Total = total;
            }
            ViewBag.Start = false;
            started = false;
            ViewBag.End = true;
            ended = true;
            stopwatch.Reset();
            return View();
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
