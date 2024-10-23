using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Object
{
    internal class CuaHang
    {
        private int maCH;
        private string tenCH;
        private string diaChi;
        private string SDT; 

        public CuaHang(int maCH, string tenCH, string diaChi, string SDT)
        {
            this.maCH = maCH;
            this.tenCH = tenCH;
            this.diaChi = diaChi;
            this.SDT = SDT;
        }
    }
}
