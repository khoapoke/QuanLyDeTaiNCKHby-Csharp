using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLDeTaiNCKH;
using BLL_QLDeTaiNCKH;
namespace GUI_QLDeTaiNCKH
{
    public class test
    {
        static void Main(string[] args)
        {

            DeTaiBLL dtbll = new DeTaiBLL();
            List<DeTaiDTO> dsdt = dtbll.layDanhSachXML(@"D:\CT ĐẠI HỌC 23-27\Lập trình hướng đối tượng\Quản Lý Đề Tài NCKH\DanhSachDeTaiNCKH.xml");

            foreach(DeTaiDTO dt in dsdt )
            {
                if (dt is NghienCuuCongNgheDTO)
                    Console.WriteLine("{0}", dt.layThuocTinh<string>());
                if(dt is NghienCuuKinhTeDTO)
                    Console.WriteLine("{0}", dt.layThuocTinh<int>());
                if(dt is NghienCuuLyThuyetDTO)
                    Console.WriteLine("{0}", dt.layThuocTinh<bool>());
            }
            

            Console.ReadKey();

        }
    }
}
