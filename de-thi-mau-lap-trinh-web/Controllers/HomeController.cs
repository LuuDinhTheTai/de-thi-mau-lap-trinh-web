using de_thi_mau_lap_trinh_web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace de_thi_mau_lap_trinh_web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly QlhangHoaContext _context;
    private readonly IWebHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, QlhangHoaContext context, IWebHostEnvironment env)
    {
        _logger = logger;
        _context = context;
        _env = env;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Lưu Đình Thế Tài";

        var model = _context.HangHoas
            .Where(h => h.Gia >= 100)
            .ToList();

        return View(model);
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

    public IActionResult FilterProducts(int? maLoai)
    {
        List<HangHoa> model;

        if (maLoai == null)
        {
            model = _context.HangHoas.Where(h => h.Gia >= 100).ToList();
        }
        else
        {
            model = _context.HangHoas
                            .Where(h => h.MaLoai == maLoai)
                            .ToList();
        }

        return PartialView("_ProductList", model);
    }

    [HttpGet]
    public IActionResult CreateView()
    {
        var categories = _context.LoaiHangs.ToList();
        ViewBag.MaLoai = new SelectList(categories, "MaLoai", "TenLoai");
        return View();  
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var categories = _context.LoaiHangs.ToList();

        if (!ModelState.IsValid)
        {
            ViewBag.MaLoai = new SelectList(categories, "MaLoai", "TenLoai");
            return View("CreateView");
        }

        // Save uploaded file to wwwroot/images
        string? savedFileName = null;
        if (request.Anh != null && request.Anh.Length > 0)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var extension = Path.GetExtension(request.Anh.FileName);
            var uniqueName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Anh.CopyToAsync(stream);
            }

            savedFileName = uniqueName;
        }

        HangHoa hangHoa = new HangHoa
        {
            MaLoai = request.MaLoai,
            TenHang = request.TenHang,
            Gia = request.Gia,
            Anh = savedFileName
        };

        _context.Add(hangHoa);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}