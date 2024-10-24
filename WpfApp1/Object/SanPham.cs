using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Object
{
    internal class SanPham
    {
                private int maSP;
                private string tenSP;
                private int donGia;
                private string moTa;
                private int maLoaiSP;

                public SanPham(int maSP, string tenSP,  int donGia, string moTa, int maLoaiSP)
                {
                        this.maSP = maSP;
                        this.tenSP = tenSP;
                        this.donGia = donGia;
                        this.moTa = moTa;
                        this.maLoaiSP = maLoaiSP;
                }
    }
}
