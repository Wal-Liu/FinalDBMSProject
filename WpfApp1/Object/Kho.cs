using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Object
{
    internal class Kho
    {
        private int maKho;
        private string tenKho;
        private string diaChi;
        private string SDT;
        Kho(int maKho, string tenKho, string diaChi, string SDT)
        {
            this.maKho = maKho;
            this.tenKho = tenKho;  
            this.diaChi = diaChi;
            this.SDT = SDT;
        }
    }
}
