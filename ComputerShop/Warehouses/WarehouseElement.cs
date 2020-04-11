using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class WarehouseElement
    {
        public byte[] PhotoData { get; set; }
        public string Product { get; set; }
        public string Artikul { get; set; }
        public int Quant { get; set; }

        public WarehouseElement(byte[] data, string prod, string art, int quan)
        {
            this.PhotoData = data;
            this.Product = prod;
            this.Artikul = art;
            this.Quant = quan;
        }
    }
}
