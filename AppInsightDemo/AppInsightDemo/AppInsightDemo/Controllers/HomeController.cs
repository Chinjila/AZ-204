using AppInsightDemo.Models;
using Azure.Storage.Blobs;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppInsightDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobServiceClient _blobClient;
        private readonly TelemetryClient telemetry;

        public HomeController(ILogger<HomeController> logger,BlobServiceClient blobClient, TelemetryClient telemetry)
        {
            _logger = logger;
            _blobClient = blobClient;
            this.telemetry = telemetry;
        }

        public IActionResult Index()
        {
            //_blobClient.CreateBlobContainer("vccontainer");
           
            telemetry.TrackException(new ArgumentException());

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
