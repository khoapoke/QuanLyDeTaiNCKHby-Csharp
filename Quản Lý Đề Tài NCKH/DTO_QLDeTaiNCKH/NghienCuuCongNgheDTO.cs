using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLDeTaiNCKH
{
    public class NghienCuuCongNgheDTO:DeTaiDTO, IphiNghienCuu
    {
        private string moiTruong;
        public string MoiTruong
        { 
            get { return moiTruong; }
            set
            {
                string[] tenMT = { "web", "mobile", "windows" };
                if (tenMT.Any(value.Trim().ToLower().Contains))
                    moiTruong = value;
                else
                    moiTruong = "Web";

            }
        
        
        }


        public NghienCuuCongNgheDTO() { }
        public NghienCuuCongNgheDTO(string id,string ten,string tentn,string gv,string mt):base (id,ten,tentn,gv)
        {

            this.MoiTruong = mt;

        }

        public override double tinhKinhPhi()
        {
            return (MoiTruong == "Web" || MoiTruong == "Mobile" ? 15000 : 10000) * hst;

        }
        public double tinhPhiNghienCuuHoTro()
        {
            return MoiTruong == "Mobile" ? 1000 : MoiTruong == "Web" ? 800 : 500;
        }

       
        public override T layThuocTinh<T>()
        {
            return (T)(object)MoiTruong;

        }

    }
}
