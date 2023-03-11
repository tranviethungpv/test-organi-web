using Organi.Models;

namespace Organi.Repository
{
    public class LoaiSpRepository
    {
        private readonly QlbanVaLiContext _context = new QlbanVaLiContext();
        public LoaiSpRepository() { }
        public LoaiSpRepository(QlbanVaLiContext context)
        {
            _context = context;
        }
        public IEnumerable<TLoaiSp> GetAll()
        {
            return _context.TLoaiSps;
        }
    }
}
