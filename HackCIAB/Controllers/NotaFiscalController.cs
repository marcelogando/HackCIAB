using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackCIAB.Controllers
{
    public class NotaFiscalController : Controller
    {
        // GET: NotaFiscal
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string OCR()
        {
            return ""; 
        }
    }
}