using System.Collections.Generic;

namespace SmartBearServer.Services.Implementations
{
    public class CandyPack
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
    }

    public static class CandyPackProvider
    {
        public static readonly List<CandyPack> Packs = new List<CandyPack>
        {
            new CandyPack { Id = 1, Count = 50, Price = 20000, Name = "Gói 50 Kẹo Bổ Sung" },
            new CandyPack { Id = 2, Count = 100, Price = 40000, Name = "Gói 100 Kẹo Bổ Sung" },
            new CandyPack { Id = 3, Count = 500, Price = 180000, Name = "Gói 500 Kẹo Bổ Sung" }
        };

        public static CandyPack? GetById(int id) => Packs.Find(p => p.Id == id);
    }
}
