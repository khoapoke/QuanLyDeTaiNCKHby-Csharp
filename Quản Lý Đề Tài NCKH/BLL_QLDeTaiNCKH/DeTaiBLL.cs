using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLDeTaiNCKH;
using DAL_QLDeTaiNCKH;
using System.IO;
namespace BLL_QLDeTaiNCKH
{
    public class DeTaiBLL
    {

        DeTaiDAL deTai=new DeTaiDAL();
        

        
        public DeTaiDAL DeTai
        {
            get { return deTai; }
            set { deTai = value; }

        }

        public DeTaiBLL()
        {
            //DeTai.LSTDeTai = DeTai.docDanhSachXML(@"D:\CT ĐẠI HỌC 23-27\Lập trình hướng đối tượng\Quản Lý Đề Tài NCKH\DanhSachDeTaiNCKH.xml");
          
        }

        // xuất danh sách xml
        public List<DeTaiDTO> layDanhSachXML(string filePath)
        {
            
            DeTai.LSTDeTai = DeTai.docDanhSachXML(filePath);
            return DeTai.LSTDeTai;
        }

        
        
        /* ##################### tìm kiếm đề tài######################*/
        //biết tên đề tài
        public DeTaiDTO timKiemTheoTen(string tendt)
        {
            
            return DeTai.LSTDeTai.Find(t => t.TenDT == tendt);

        }

        //biết mã số
        public DeTaiDTO timKiemTheoMa(string ma)
        {
            return DeTai.LSTDeTai.Find(t => t.Id == ma);
        }

        //biết tên trưởng nhóm
        public List<DeTaiDTO> timKiemTheoTenTruongNhom(string tenTruongN)
        {
            string kq = tenTruongN.Trim().ToLower();
            return DeTai.LSTDeTai.Where(dt=>string.Equals(dt.TruongNhom,tenTruongN,StringComparison.OrdinalIgnoreCase)||dt.TruongNhom.Trim().ToLower().Contains(kq)).ToList();
        }

        

        // Tìm kiếm đề tài theo tên giáo viên
        public List<DeTaiDTO> timKiemTheoGiaoVien(string gv)
        {
            string kq = gv.Trim().ToLower();
            return DeTai.LSTDeTai.Where(dt => string.Equals(dt.GiaoVien, gv, StringComparison.OrdinalIgnoreCase)||dt.GiaoVien.Trim().ToLower().Contains(kq)).ToList();
        }


        // xuất danh sách đề tài trên 10 triệu
        public List<DeTaiDTO> dsDeTaiTren10tr()
        {

            return DeTai.LSTDeTai.Where(t => t.tinhKinhPhi() > 10000).ToList();
        }


        //xuất danh sách các đề tài thuộc lĩnh vực kinh tế 
        
        public List<NghienCuuLyThuyetDTO> dsDeTaiLyThuyet()
        {

            return DeTai.LSTDeTai.OfType<NghienCuuLyThuyetDTO>().Where(t => t.ApDungThucTe == true).ToList();
        }
        // xuất danh sách có trên 100 câu hỏi
        public List<NghienCuuKinhTeDTO> dsDeTaiKinhte()
        {

            return DeTai.LSTDeTai.OfType<NghienCuuKinhTeDTO>().Where(t => t.SoCauHoi >= 100).ToList();
        }
        // xuất danh sách có thời gian phát triển trên 4 tháng
        public List<NghienCuuCongNgheDTO> dsDeTaiCongNghe()
        {
            return DeTai.LSTDeTai.OfType<NghienCuuCongNgheDTO>().Where(t => t.MoiTruong.Trim().ToLower() == "windows").ToList();
        }
        // gọi tầng DAL để ghi lại danh sách
        public  void luuDanhSach(List<DeTaiDTO> ds,string filePath)
        {

            DeTai.luuVaoFile(ds, filePath);
        }

    }
}
