using Microsoft.AspNetCore.Mvc;
using Organi.Repository;

namespace Organi.ViewComponents
{
    public class MenuLoaiSpViewComponent : ViewComponent
    {
        private readonly LoaiSpRepository _loaiSpRepository = new LoaiSpRepository();
        public MenuLoaiSpViewComponent() { }
        public IViewComponentResult Invoke()
        {
            var loaiSp = _loaiSpRepository.GetAll();
            return View(loaiSp);
        }
    }
}
