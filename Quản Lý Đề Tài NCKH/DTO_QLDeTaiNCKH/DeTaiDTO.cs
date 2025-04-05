using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLDeTaiNCKH
{
    public abstract class DeTaiDTO
    {

        protected string id;
        protected string tenDT;
        protected string truongNhom;
        protected string giaoVien;
        public static double hst = 1.0;
        public static void capNhapKinhPhi(double tl)
        {
            hst = hst *tl; // CẬP NHẬT KINH CỘNG DỒN VỚI KINH PHÍ HIỆN TẠI
        }
        public string Id
        {

            get { return id; }
            set {

                string[] nc = {"LT","KT","CN" };
                if (value.Length == 6 && value.StartsWith("NC") && nc.Contains(value.Substring(2, 2)) && value.Substring(4).All(char.IsDigit))
                    id = value;
                else
                    id = "NCDT00";
            }

        }

        public string TenDT
        {
            get { return tenDT; }
            set
            {
                tenDT = value;
            }
        }

       

        public string TruongNhom
        {
            get { return truongNhom; }
            set { truongNhom = value; }

        }

        public string GiaoVien
        {
            get { return giaoVien; }
            set { giaoVien = value; }
        }

        public DeTaiDTO()
        {
            
        }

        public DeTaiDTO(string id,string ten,string tNhom,string gv)
        {
            this.Id = id;
            this.TenDT = ten;
            
            this.TruongNhom = tNhom;
            this.GiaoVien=gv;

        }


        public abstract double tinhKinhPhi();

        public abstract T layThuocTinh<T>(); // dùng để lấy các thuộc tính khác cho datagridview
        

       
    }

 
}
