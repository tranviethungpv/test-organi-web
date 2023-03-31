using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Organi.Models;
using Organi.Models.Authenciation;
using X.PagedList;

namespace Organi.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [Route("")]
        [Route("index")]
        [Authenciation]
        public IActionResult Index() => View();
        [Route("danhmucsanpham")]
        [Authenciation]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanpham, pageNumber, pageSize);
            return View(lst);
        }
        [Route("themsanpham")]
        [HttpGet]
        [Authenciation]
        public IActionResult ThemSanPham()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            return View();
        }
        [Route("themsanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authenciation]
        public IActionResult ThemSanPham(TDanhMucSp sanpham)
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanpham);
        }
        [Route("suasanpham")]
        [HttpGet]
        [Authenciation]
        public IActionResult SuaSanPham(string masp)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var sanpham = db.TDanhMucSps.Find(masp);
            return View(sanpham);
        }
        [Route("suasanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authenciation]
        public IActionResult SuaSanPham(TDanhMucSp sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanpham);
        }
        [Route("xoasanpham")]
        [HttpGet]
        [Authenciation]
        public IActionResult XoaSanPham(string masp)
        {
            TempData["Message"] = "";
            var chitietsanpham = db.TChiTietSanPhams.Where(x => x.MaSp == masp).ToList();
            if(chitietsanpham.Count() > 0)
            {
                TempData["Message"] = "Không xoá được sản phẩm này";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            var anhsp = db.TAnhSps.Where(x => x.MaSp == masp).ToList();
            if (anhsp.Any()) db.RemoveRange(anhsp);
            db.Remove(db.TDanhMucSps.Find(masp));
            db.SaveChanges();
            TempData["Message"] = "Xoá sản phẩm thành công";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }
    }
}
