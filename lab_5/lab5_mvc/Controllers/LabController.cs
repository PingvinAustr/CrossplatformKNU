using lab5_labs;
using lab5_mvc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab5_mvc.Controllers
{
    [Authorize]
    public class LabController : Controller
    {
        public IActionResult Lab1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab1([FromForm] IFormFile file)
        {
            string inputText = await file.ReadAsStringAsync();
            var response = LabExecuter.ExecuteLab1(inputText);

            return View(response);
        }

        public IActionResult Lab2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab2([FromForm] IFormFile file)
        {
            string inputText = await file.ReadAsStringAsync();
            var response = LabExecuter.ExecuteLab2(inputText);

            return View(response);
        }

        public IActionResult Lab3()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab3([FromForm] IFormFile file)
        {
            string inputText = await file.ReadAsStringAsync();
            var response = LabExecuter.ExecuteLab3(inputText);

            return View(response);
        }
    }
}
