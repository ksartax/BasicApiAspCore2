using BasicApiCore.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BasicApiCore
{
    public static class CastleContextExtensions
    {
        public static void EnsureSeedDataForContext(this CastleContext castleContext)
        {
            if (castleContext.Castle.Any())
            {
                return;
            }

            var castles = new List<Castle>()
            {
                new Castle
                {
                    Name = "Sowia",
                    CastleDetails = new List<CastleDetail>()
                    {
                        new CastleDetail
                        {
                            Description = "Wysoko1"
                        },
                        new CastleDetail
                        {
                            Description = "Wysoko2"
                        }
                    }
                },
                new Castle
                {
                    Name = "Liw",
                    CastleDetails = new List<CastleDetail>()
                    {
                        new CastleDetail
                        {
                            Description = "Wysoko3"
                        },
                        new CastleDetail
                        {
                            Description = "Wysoko4"
                        }
                    }
                },
                new Castle
                {
                    Name = "Gniezno",
                    CastleDetails = new List<CastleDetail>() {}
                },
                new Castle
                {
                    Name = "Skała",
                    CastleDetails = new List<CastleDetail>() {}
                },
                new Castle
                {
                    Name = "Piła",
                    CastleDetails = new List<CastleDetail>() {}
                }
            };

            castleContext.AddRange(castles);
            castleContext.SaveChanges();

        }
    }
}
