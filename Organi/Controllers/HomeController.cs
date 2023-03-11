using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organi.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using X.PagedList;

namespace Organi.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanpham, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SanPhamTheoLoai(string maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSpTheoLoai = db.TDanhMucSps.Where(x => x.MaLoai == maloai).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSpTheoLoai, pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(lst);
        }

        public IActionResult ChiTietSanPham(string masanpham)
        {
            var product = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == masanpham);
            var imageProduct = db.TAnhSps.Where(x => x.MaSp == masanpham).ToList();
            ViewBag.imageProduct = imageProduct;
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}