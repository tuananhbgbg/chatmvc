using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChatMVC.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly MessageService service;

        public HomeController(MessageService service)
        {
            this.service = service;
        }

        public IActionResult Index(string apikey,bool hasMessage)
        {
            if (hasMessage)
            {
                ViewBag.Message = TempData["message"];
            }
            
            if (String.IsNullOrEmpty(apikey) && String.IsNullOrEmpty(service.HeaderApiKey()))
            {
                return RedirectToAction("Index", "User");
            }
            service.Register(apikey);
            var channel = service.GetChannelAndMessages(20);
            return View("Index",channel);
        }
        [HttpPost("message")]
        public IActionResult SendMessage(string content)
        {
            service.SendMsg(content);
            return RedirectToAction("Index", new { apikey = service.HeaderApiKey() });
        }
    }
}
