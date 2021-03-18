using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Controllers
{
    public class CryptoConverterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
