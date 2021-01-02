using Azure.ServiceBus.Web.Models;
using Azure.ServiceBus.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.ServiceBus.Web.Controllers
{
    public class ServiceBusQueueController : Controller
    {
        private IServiceBusQueueService _serviceBusQueueService;

        public ServiceBusQueueController(IServiceBusQueueService serviceBusQueueService)
        {
            _serviceBusQueueService = serviceBusQueueService;
        }

        public IActionResult Index(string msg)
        {
            ViewBag.Message = string.Empty;
            if (!string.IsNullOrEmpty(msg))
                ViewBag.Message = msg;
            return View();
        }

        [Route("ServiceBusQueue/Send")]
        [HttpPost()]
        public async Task<IActionResult> SendMessage(IFormCollection collection)
        {
            var msg = new Message
            {
                Id=1,
                Title = collection["Title"],
                Content = collection["Content"],
                CreateTime=DateTime.Now
            };
            await _serviceBusQueueService.SendMessageAsync(msg);
            return RedirectToAction("Index", "ServiceBusQueue",new {msg= " Success!" });
        }
    }
}
