using System.Collections.Generic;
using System.Linq;
using BasicApiCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicApiCore.Services
{
    public class CastleRepository : ICastleRepository
    {
        private readonly CastleContext _castleContext;

        public CastleRepository(CastleContext castleContext)
        {
            _castleContext = castleContext;
        }

        public IEnumerable<Castle> GetCastles()
        {
            return _castleContext.Castle.OrderBy(o => o.Name).ToList();
        }

        public Castle GetCastle(int castleId, bool includeDetails)
        {
            if (includeDetails)
            {
               return _castleContext.Castle.Include(q => q.CastleDetails).Where(q => q.Id == castleId).FirstOrDefault();
            }

            return _castleContext.Castle.Where(q => q.Id == castleId).FirstOrDefault();
        }

        public IEnumerable<CastleDetail> GetCastleDetailsForCastle(int castleId)
        {
            return _castleContext.CastleDetail.Where(q => q.CastleId == castleId).ToList();
        }

        public CastleDetail GetCastleDetailForCastle(int castleId, int castleDetailId)
        {
            return _castleContext.CastleDetail.Where(q => q.Id == castleDetailId && q.CastleId == castleId).FirstOrDefault();
        }

        public bool CastleExist(int castleId)
        {
            return _castleContext.Castle.Any(b => b.Id == castleId);
        }

        public void AddCastleDetailForCastle(int castleId, CastleDetail castleDetail)
        {
            var castle = GetCastle(castleId, false);
            castle.CastleDetails.Add(castleDetail);
        }

        public bool Save()
        {
            return (_castleContext.SaveChanges() >= 0);
        }

        public void DeleteDetail(CastleDetail castleDetail)
        {
            _castleContext.CastleDetail.Remove(castleDetail);
        }
    }
}
