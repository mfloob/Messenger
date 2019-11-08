using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Messenger.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        public static Client client = new Client(8000);

        [HttpGet("[action]")]
        public bool SendMessage(string message)
        {
            client.SendMessage($"{message}");
            return true;
        }

        [HttpGet("[action]")]
        public JsonResult Listen()
        {
            var result = client.Listen();
            if (string.IsNullOrWhiteSpace(result.Trim()))
                return Json(null);
            return Json(result);
        }
    }
}