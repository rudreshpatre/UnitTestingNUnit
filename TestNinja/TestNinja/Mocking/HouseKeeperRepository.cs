using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IHouseKeeperRepository
    {
        IQueryable<Housekeeper> GetHouseKeepers();
    }

    class HouseKeeperRepository : IHouseKeeperRepository
    {
        private UnitOfWork _unitOfWork;

        public HouseKeeperRepository()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IQueryable<Housekeeper> GetHouseKeepers()
        {
            var housekeepers = _unitOfWork.Query<Housekeeper>();
            return housekeepers;
        }
    }
}
