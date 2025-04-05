using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLDeTaiNCKH
{
    public class NghienCuuKinhTeDTO : DeTaiDTO, IphiNghienCuu
    {
        private int soCauHoi;
        public int SoCauHoi
        {
            get { return soCauHoi; }
            set
            {
                if (value < 0)
                    throw new Exception("So cau hoi phai lon hon 0");
                else
                    soCauHoi = value;
            }


        }

        public NghienCuuKinhTeDTO() { }
        public NghienCuuKinhTeDTO(string id,string ten,string tnhom,string gv,int sch):base (id,ten,tnhom,gv)
        {
            this.SoCauHoi = sch;

        }

        public override double tinhKinhPhi()
        {
            return (SoCauHoi > 100 ? 12000 : 7000)*hst;
        }


        public double tinhPhiNghienCuuHoTro()
        { return SoCauHoi > 100 ? 550 * SoCauHoi : 450 * SoCauHoi; }

        

        public override T layThuocTinh<T>()
        {
            return (T)(object)SoCauHoi;

        }
    }
}
