using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLDeTaiNCKH;
using System.Xml;


namespace DAL_QLDeTaiNCKH
{
    public class DeTaiDAL
    {

        List<DeTaiDTO> lstDeTai = new List<DeTaiDTO>();


        public List<DeTaiDTO> LSTDeTai
        {

            get { return lstDeTai; }
            set
            {
                lstDeTai = value;
            }

        }


        public List<DeTaiDTO> docDanhSachXML(string filePath)
        {
            

                XmlDocument read = new XmlDocument();

                read.Load(filePath);


                XmlNodeList ncltList = read.SelectNodes("DanhSachDeTaiNCKH/DeTaiNCLT");
                foreach (XmlNode node in ncltList)
                {
                    NghienCuuLyThuyetDTO nclt = new NghienCuuLyThuyetDTO();
                    nclt.Id = node.Attributes["id"].Value;
                    nclt.TenDT = node["TenDT"].InnerText;

                    nclt.TruongNhom = node["TruongNhom"].InnerText;
                    nclt.GiaoVien = node["GiaoVien"].InnerText;
                    nclt.ApDungThucTe = bool.Parse(node["ApDungThucTe"].InnerText);
                    LSTDeTai.Add(nclt);


                }
                XmlNodeList ncktList = read.SelectNodes("DanhSachDeTaiNCKH/DeTaiNCKT");
                foreach (XmlNode node in ncktList)
                {
                    NghienCuuKinhTeDTO nckt = new NghienCuuKinhTeDTO();
                    nckt.Id = node.Attributes["id"].Value;
                    nckt.TenDT = node["TenDT"].InnerText;

                    nckt.TruongNhom = node["TruongNhom"].InnerText;
                    nckt.GiaoVien = node["GiaoVien"].InnerText;
                    nckt.SoCauHoi = int.Parse(node["SoCauHoi"].InnerText);

                    LSTDeTai.Add(nckt);



                }

                XmlNodeList nccnList = read.SelectNodes("DanhSachDeTaiNCKH/DeTaiNCCN");
                foreach (XmlNode node in nccnList)
                {
                    NghienCuuCongNgheDTO nccn = new NghienCuuCongNgheDTO();

                    nccn.Id = node.Attributes["id"].Value;
                    nccn.TenDT = node["TenDT"].InnerText;

                    nccn.TruongNhom = node["TruongNhom"].InnerText;
                    nccn.GiaoVien = node["GiaoVien"].InnerText;
                    nccn.MoiTruong = node["MoiTruong"].InnerText;
                    LSTDeTai.Add(nccn);


                }

                return LSTDeTai;
           
        }

        public  void luuVaoFile(List<DeTaiDTO> ds,string filePath)
        {
            XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true });
            writer.WriteStartDocument();
            writer.WriteStartElement("DanhSachDeTaiNCKH");
            foreach( DeTaiDTO dt in ds)
            {
                if(dt is NghienCuuLyThuyetDTO )
                {
                    writer.WriteStartElement("DeTaiNCLT");
                    writer.WriteAttributeString("id", dt.Id);
                    writer.WriteElementString("TenDT", dt.TenDT);
                    writer.WriteElementString("TruongNhom", dt.TruongNhom);
                    writer.WriteElementString("GiaoVien", dt.GiaoVien);
                    writer.WriteElementString("ApDungThucTe", dt.layThuocTinh<bool>().ToString());
                    
                }
                if (dt is NghienCuuKinhTeDTO)
                {
                    writer.WriteStartElement("DeTaiNCKT");
                    writer.WriteAttributeString("id", dt.Id);
                    writer.WriteElementString("TenDT", dt.TenDT);
                    writer.WriteElementString("TruongNhom", dt.TruongNhom);
                    writer.WriteElementString("GiaoVien", dt.GiaoVien);
                    writer.WriteElementString("SoCauHoi", dt.layThuocTinh<int>().ToString());
                    
                }
                if (dt is NghienCuuCongNgheDTO)
                {
                    writer.WriteStartElement("DeTaiNCCN");
                    writer.WriteAttributeString("id", dt.Id);
                    writer.WriteElementString("TenDT", dt.TenDT);
                    writer.WriteElementString("TruongNhom", dt.TruongNhom);
                    writer.WriteElementString("GiaoVien", dt.GiaoVien);
                    writer.WriteElementString("MoiTruong", dt.layThuocTinh<string>());
                    
                }
                writer.WriteEndElement();

            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

      

    }
}
