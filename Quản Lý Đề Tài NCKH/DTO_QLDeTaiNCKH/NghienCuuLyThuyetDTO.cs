using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLDeTaiNCKH
{
    public class NghienCuuLyThuyetDTO : DeTaiDTO
    {
        private bool apDungThucTe;

        public bool ApDungThucTe
        {
            get { return apDungThucTe; }
            set { 
                
                
                     apDungThucTe = (bool)value; 
            
            }

        }

        public NghienCuuLyThuyetDTO()
        { }

        public NghienCuuLyThuyetDTO(string id, string tenDT, string truongNhom, string giaoVien, bool ADTT)
            : base(id, tenDT, truongNhom, giaoVien)
        {
            this.ApDungThucTe = ADTT;

        }

        public override double tinhKinhPhi()
        {
            return (ApDungThucTe == true ? 15000 : 8000)*hst;
        }

       
        public override T layThuocTinh<T>()
        {

            return (T)(object)ApDungThucTe;
        }
       


    }
}
