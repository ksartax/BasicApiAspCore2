using BasicApiCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicApiCore.Services
{
    public interface ICastleRepository
    {
        bool CastleExist(int castleId);
        IEnumerable<Castle> GetCastles();
        Castle GetCastle(int castleId, bool includeDetails);
        IEnumerable<CastleDetail> GetCastleDetailsForCastle(int castleId);
        CastleDetail GetCastleDetailForCastle(int castleId, int castleDetailId);
        void AddCastleDetailForCastle(int castleId, CastleDetail castleDetail);
        void DeleteDetail(CastleDetail castleDetail);
        bool Save();
    }
}
