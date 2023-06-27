using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using JobApplication.Data;
using JobApplication.Models;
using JobApplication.Services;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Resources;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class HomeController : Controller
{
    private readonly IRepository _repository;
    public HomeController(IRepository repository, ILogger<HomeController> logger)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult CultureManagement(string culture)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Index(JobApplication.Models.JobApplication model, IFormFile postedFile)
    {
        if (postedFile == null || postedFile.Length == 0)
        {
            var resm = new ResourceManager("JobApplication.Resources.Controllers.HomeController", typeof(HomeController).Assembly);
            ModelState.AddModelError("FileUpload", resm.GetString("NoCV"));
        }
        else
        {
            var fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);
            var extension = Path.GetExtension(postedFile.FileName);
            if (extension != ".pdf" && extension != ".txt" && extension != ".doc" && extension != ".docx")
            {
                var resm = new ResourceManager("JobApplication.Resources.Controllers.HomeController", typeof(HomeController).Assembly);
                ModelState.AddModelError("FileUpload",resm.GetString("ExtensionError"));
            }
            else
            {
                var fileModel = new FileModel
                {
                    CreatedOn = DateTime.UtcNow,
                    FileType = postedFile.ContentType,
                    Extension = extension,
                    Name = fileName
                };

                using (var dataStream = new MemoryStream())
                {
                    await postedFile.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }

                if (fileModel.Data.Length > 5 * 1024 * 1024)
                {
                    var resm = new ResourceManager("JobApplication.Resources.Controllers.HomeController", typeof(HomeController).Assembly);
                    ModelState.AddModelError("FileUpload", resm.GetString("DataError"));
                }

                model.FileUpload = fileModel;
            }
        }

        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
            }

            return View();
        }

        await _repository.CreateApplicant(model);
        return RedirectToAction("Index");
    }

}
