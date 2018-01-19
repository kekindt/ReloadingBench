using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using ReloadingBench;

namespace ReloadingBench
{
    public class BsonBase
    {
        [BsonId]
        public ObjectId ID { get; set; }
    }

    public class Cartridge : BsonBase
    {
        private Primer primer = new Primer();
        private Powder powder = new Powder();
        private Bullet bullet = new Bullet();

        public string Name { get; set; }

        public string CaseBrand { get; set; }

        public string PrimerId
        {
            get
            {
                if (primer != null)
                    return primer.ID.ToString();
                else
                    return string.Empty;
            }
            set
            {
                if (primer == null)
                {
                    primer = new Primer();
                }
                if (!string.IsNullOrEmpty(value))
                    primer.ID = new ObjectId(value);
            }
        }

        [BsonIgnore]
        public Primer Primer
        {
            get { return primer; }
            set
            {
                primer = value;
            }
        }

        public string PowderId
        {
            get
            {
                if (powder != null)
                    return powder.ID.ToString();
                else
                    return string.Empty;
            }
            set
            {
                if (powder == null)
                {
                    powder = new Powder();
                }
                powder.ID = new ObjectId(value);
            }
        }

        [BsonIgnore]
        public Powder Powder
        {
            get
            {
                return powder;
            }
            set
            {
                powder = value;
            }
        }

        public decimal Charge { get; set; }

        public string BulletId
        {
            get
            {
                if (bullet != null)
                    return bullet.ID.ToString();
                else
                    return string.Empty;
            }
            set
            {
                if (bullet == null)
                {
                    bullet = new Bullet();
                }
                bullet.ID = new ObjectId(value);
            }
        }

        [BsonIgnore]
        public Bullet Bullet { get; set; }
    }

    public class Primer : BsonBase
    {
        public string Brand { get; set; }

        public string Size { get; set; }

        [BsonIgnore]
        public string Description
        {
            get
            {
                return $"{Brand} - {Size}";
            }
        }
    }

    public class Powder : BsonBase
    {
        public string Brand { get; set; }

        public string Name { get; set; }

        [BsonIgnore]
        public string Description
        {
            get
            {
                return $"{Brand} - {Name}";
            }
        }
    }

    public class Bullet : BsonBase
    {
        public string Brand { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public string Calibur { get; set; }

        [BsonIgnore]
        public string Description
        {
            get
            {
                return $"{Calibur} - {Weight} - {Brand} - {Name}";
            }
        }
    }

    public class Lot : BsonBase
    {
        public Lot()
        {
            LoadedDate = DateTime.Now;
        }

        Cartridge cartridge = new Cartridge();

        public DateTime LoadedDate { get; set; }

        public string CartridgeId
        {
            get
            {
                if (cartridge != null)
                    return cartridge.ID.ToString();
                else
                    return string.Empty;
            }
            set
            {
                if (cartridge == null)
                {
                    cartridge = new Cartridge();
                }
                cartridge.ID = new ObjectId(value);
            }
        }

        [BsonIgnore]
        public Cartridge Cartridge
        {
            get
            {
                return cartridge;
            }
            set
            {
                cartridge = value;
            }
        }

        public int CountLoaded { get; set; }

        public int SpentCount { get; set; }

        public DateTime SpentDate { get; set; }

        public string Comments { get; set; }
    }
}