using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QLDeTaiNCKH;
using DTO_QLDeTaiNCKH;
namespace GUIFORM_QLDeTaiNCKH
{
    public partial class QLDeTaiNCKH : Form
    {

        DeTaiBLL dtBLL = new DeTaiBLL();
        List<DeTaiDTO> DSDT;
        public static string  FilePath = "";
        public QLDeTaiNCKH()
        {
            InitializeComponent();
        }
        private void capNhapTrangThaiNut()
        {
            bool coDuLieu = DSDT != null && DSDT.Count > 0 ? true : false;


            mnuFileXuat.Enabled = coDuLieu;
            mnuFileLuu.Enabled = coDuLieu;
            btnThem.Enabled = coDuLieu;
            btnTimKiem.Enabled = coDuLieu;
            btnCapNhap.Enabled = coDuLieu;
            btnXuatKPtren10TR.Enabled = coDuLieu;
            btnXuatLVLTvaTRUE.Enabled = coDuLieu;
            btnXuatPT4TH.Enabled = coDuLieu;
            btnXuatSCHtren100.Enabled = coDuLieu;
            
        }

        private bool DanhSachRong()
        {
            if(DSDT==null||DSDT.Count==0)
            {
                
                return true;
                    
            }
            return false;
        }
        // load form
        private void QLDeTaiNCKH_Load(object sender, EventArgs e)
        {
            cmbboxTimKiem.Text = "ID";
            cmbboxThuocTinh.Text = "Áp dụng thực tế";
            cmbboxKinhPhi.Text = "10%";
            capNhapTrangThaiNut();
            if(DanhSachRong())
            {
                MessageBox.Show("Danh sách đang rỗng hãy thêm chọn tệp", "Thông báo");
                return;

            }
        }
        
        
        // chọn file
        private void mnuFileChon_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Văn bản|*.xml";
            openFileDialog.Title = "Chọn một tệp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
                MessageBox.Show("Bạn đã chọn: " + FilePath, "Thông báo");
                DSDT = dtBLL.layDanhSachXML(FilePath);
                capNhapTrangThaiNut();
                return;

            }
        }
        

        //xuất danh sách
        private void xuatDuLieu(DeTaiDTO dt)
        {


            if (dt is NghienCuuLyThuyetDTO)
            {
                
                dtaDanhSachDeTai.Rows.Add(dt.Id, dt.TenDT, dt.TruongNhom, dt.GiaoVien, dt.tinhKinhPhi(), 0, "Áp dụng thực Tế:" + dt.layThuocTinh<bool>());
            }
            else if (dt is NghienCuuKinhTeDTO)
            {
                NghienCuuKinhTeDTO nckt = dt as NghienCuuKinhTeDTO; // ép kiểu cho framework 4.5 
                dtaDanhSachDeTai.Rows.Add(dt.Id, dt.TenDT, dt.TruongNhom, dt.GiaoVien, dt.tinhKinhPhi(), nckt.tinhPhiNghienCuuHoTro(), "Số câu hỏi:" + dt.layThuocTinh<int>());
            }
            else if (dt is NghienCuuCongNgheDTO)
            {
                NghienCuuCongNgheDTO nccn = dt as NghienCuuCongNgheDTO;
                dtaDanhSachDeTai.Rows.Add(dt.Id, dt.TenDT, dt.TruongNhom, dt.GiaoVien, dt.tinhKinhPhi(), nccn.tinhPhiNghienCuuHoTro(),"Môi trường:" + dt.layThuocTinh<string>());
            }

            else
                dtaDanhSachDeTai.Rows.Add(dt.Id, dt.TenDT, dt.TruongNhom, dt.GiaoVien,dt.tinhKinhPhi(),0,"");

        }

        // xuất toàn bộ danh sách vào bảng DataGridView
        private void mnuFileXuat_Click(object sender, EventArgs e)
        {


            if (DanhSachRong())
            {
                MessageBox.Show("Danh sách đang rỗng hãy thêm thông tin", "Thông báo");
                return;

            }

           dtaDanhSachDeTai.Rows.Clear();

            foreach (DeTaiDTO dt in DSDT)
            {
                xuatDuLieu(dt);
            }

        }
        

        
        // nút xóa trong danh sách 
        private void dtaDanhSachDeTai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if(dtaDanhSachDeTai.Columns[e.ColumnIndex].Name=="Delete")
            {
                if(MessageBox.Show("Bạn có muốn xóa đề tài này không ?","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    dtaDanhSachDeTai.Rows.RemoveAt(e.RowIndex);
                    DSDT.RemoveAt(e.RowIndex);
                }
            }

        }
        // kiểm tra ô thuộc tính
        private void txtboxThuocTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbboxThuocTinh.Text == "Số câu hỏi" && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool kiemTraTrueFalse(string str)
        {

            if (string.Equals(str, "True", StringComparison.OrdinalIgnoreCase))
                return true;
            if (string.Equals(str, "False", StringComparison.OrdinalIgnoreCase))
                return false;
            MessageBox.Show("Chỉ được nhập True hoặc False (Mặc định là False)", "Thông báo");
            return false;

        }
       
        // kiểm tra mã đã tồn tại
        private bool kiemTraMaTrung()
        {
            string id = txtboxID.Text;
            foreach( DeTaiDTO dt in DSDT)
            {
                

                if(dt.Id==id)
                {
                   
                    return true;

                }
            }
            return false;

        }
        //Thêm mới 1 đề tài
        private void btnThem_Click(object sender, EventArgs e)
        {
            
            if (cmbboxThuocTinh.Text == "" || txtboxThuocTinh.Text == "" || txtboxID.Text == "" || txtboxTenDT.Text == "" || txtboxTN.Text == "" || txtboxGV.Text=="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
                return;
            }
            if(kiemTraMaTrung())
            {
                MessageBox.Show("ID đã tồn tại!", "Thông báo");
                return;
            }
            if (cmbboxThuocTinh.Text == "Số câu hỏi")
            {
                NghienCuuKinhTeDTO nckt = new NghienCuuKinhTeDTO();
                nckt.Id = txtboxID.Text;
                nckt.TenDT = txtboxTenDT.Text;
                nckt.TruongNhom = txtboxTN.Text;
                nckt.GiaoVien = txtboxGV.Text;
                nckt.SoCauHoi = int.Parse(txtboxThuocTinh.Text);
                dtaDanhSachDeTai.Rows.Add(nckt.Id, nckt.TenDT, nckt.TruongNhom, nckt.GiaoVien, nckt.tinhKinhPhi(),nckt.tinhPhiNghienCuuHoTro()," Số câu hỏi:"+nckt.SoCauHoi);
                DSDT.Add(nckt);
                
            }
            if(cmbboxThuocTinh.Text=="Môi trường")
            {
                NghienCuuCongNgheDTO nccn = new NghienCuuCongNgheDTO();
                nccn.Id = txtboxID.Text;
                nccn.TenDT = txtboxTenDT.Text;
                nccn.TruongNhom = txtboxTN.Text;
                nccn.GiaoVien = txtboxGV.Text;
                nccn.MoiTruong = txtboxThuocTinh.Text;
                dtaDanhSachDeTai.Rows.Add(nccn.Id, nccn.TenDT, nccn.TruongNhom, nccn.GiaoVien, nccn.tinhKinhPhi(),nccn.tinhPhiNghienCuuHoTro(), "Môi trường:"+nccn.MoiTruong);
                DSDT.Add(nccn);
                
            }
            if(cmbboxThuocTinh.Text=="Áp dụng thực tế")
            {
                NghienCuuLyThuyetDTO nclt = new NghienCuuLyThuyetDTO();
                nclt.Id = txtboxID.Text;
                nclt.TenDT = txtboxTenDT.Text;
                nclt.TruongNhom = txtboxTN.Text;
                nclt.GiaoVien = txtboxGV.Text;
                nclt.ApDungThucTe = kiemTraTrueFalse(txtboxThuocTinh.Text);
                dtaDanhSachDeTai.Rows.Add(nclt.Id, nclt.TenDT, nclt.TruongNhom, nclt.GiaoVien, nclt.tinhKinhPhi(),0, "Áp dụng thực tế:" + nclt.ApDungThucTe);
                DSDT.Add(nclt);
                
            }
            txtboxID.Text = "";
            txtboxTenDT.Text = "";
            txtboxTN.Text = "";
            txtboxGV.Text = "";
            txtboxThuocTinh.Text = "";
            
        }


        //Tìm kiếm thông tin đề tài
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtboxTimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
                return;
            }
            if (DanhSachRong())
            {
                MessageBox.Show("Danh sách đang rỗng hãy thêm thông tin", "Thông báo");
                return;

            }
            // theo  ID trả về 1 đối tượng thay vì danh sách
            if(cmbboxTimKiem.Text=="ID")
            {

                dtaDanhSachDeTai.Rows.Clear();
                DeTaiDTO kq = dtBLL.timKiemTheoMa(txtboxTimKiem.Text);
                if (kq == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin", "Thông báo");
                    return;
                }
                else
                {
                    xuatDuLieu(kq);
                    return;
                }
            }
            // theo tên đề tài trả về 1 đối tượng
            if (cmbboxTimKiem.Text == "Tên Đề Tài")
            {
                dtaDanhSachDeTai.Rows.Clear();
                DeTaiDTO kq = dtBLL.timKiemTheoTen(txtboxTimKiem.Text);
                if (kq == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin", "Thông báo");
                    return;
                }
                else
                {
                    xuatDuLieu(kq);
                    return;
                }
            }
            //theo tên trưởng nhóm trả về danh sách
            if(cmbboxTimKiem.Text=="Trưởng Nhóm")
            {
                dtaDanhSachDeTai.Rows.Clear();
                List < DeTaiDTO > ds1= dtBLL.timKiemTheoTenTruongNhom(txtboxTimKiem.Text);
                Console.WriteLine("{0}",txtboxTimKiem.Text.Trim().ToLower());
                if (ds1.Count == 0)
                {

                    MessageBox.Show("Không tìm thấy thông tin", "Thông báo");
                    return;
                }
                foreach(DeTaiDTO dt in ds1)
                {
                    xuatDuLieu(dt);
                }
                return;

            }
            // theo tên giảng viên trả về danh sách
            if(cmbboxTimKiem.Text=="Giáo Viên")
            {
                dtaDanhSachDeTai.Rows.Clear();
                List<DeTaiDTO> ds1 = dtBLL.timKiemTheoGiaoVien(txtboxTimKiem.Text);
                if(ds1.Count==0)
                {

                    MessageBox.Show("Không tìm thấy thông tin", "Thông báo");
                    return;
                }
                foreach (DeTaiDTO dt in ds1)
                {
                    xuatDuLieu(dt);
                }
                return;

            }
           
        }

        // Lưu lại danh sách vào tệp
        private void mnuFileLuu_Click(object sender, EventArgs e)
        {
            if(DSDT==null)
            {
                DialogResult kq = MessageBox.Show("Danh sách đang rỗng, bạn có muốn lưu không?","Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.No)
                    return;

            }
            dtBLL.luuDanhSach(DSDT, FilePath);
            MessageBox.Show("Lưu tệp thành công", "Thông báo");
           
        }

        // Xuất danh sách kinh phí trên 10 triệu
        private void btnXuatKPtren10TR_Click(object sender, EventArgs e)
        {
            if (DanhSachRong())
            {
                MessageBox.Show("Danh sách đang rỗng hãy thêm thông tin", "Thông báo");
                return;

            }
            dtaDanhSachDeTai.Rows.Clear();
            List<DeTaiDTO> ds = dtBLL.dsDeTaiTren10tr();
            foreach(DeTaiDTO dt in ds)
            {
                xuatDuLieu(dt);
            }
        }
        //xuất đề tài lĩnh vực lý thuyết và có khả năng áp dụng
        private void btnXuatLVLTvaTRUE_Click(object sender, EventArgs e)
        {
            if (DanhSachRong())
            {
                MessageBox.Show("Danh sách đang rỗng hãy thêm thông tin", "Thông báo");
                return;

            }
            dtaDanhSachDeTai.Rows.Clear();
            List<NghienCuuLyThuyetDTO> ds = dtBLL.dsDeTaiLyThuyet();
            foreach(NghienCuuLyThuyetDTO dt in ds)
            {
                
                xuatDuLieu(dt);
            }
        }
        //xuất danh sách có câu hỏi trên 100 câu
        private void btnXuatSCHtren100_Click(object sender, EventArgs e)
        {
            if (DanhSachRong())
            {
                MessageBox.Show("Danh sách đang rỗng hãy thêm thông tin", "Thông báo");
                return;

            }
            dtaDanhSachDeTai.Rows.Clear();
            List<NghienCuuKinhTeDTO> ds = dtBLL.dsDeTaiKinhte();
            foreach(NghienCuuKinhTeDTO dt in ds)
            {
                xuatDuLieu(dt);
            }
        }
        // xuất đề tài phát triển trên 4 tháng {Window-environment}
        private void btnXuatPT4TH_Click(object sender, EventArgs e)
        {
            if (DanhSachRong())
            {
                MessageBox.Show("Danh sách đang rỗng hãy thêm thông tin", "Thông báo");
                return;

            }
            dtaDanhSachDeTai.Rows.Clear();
            List<NghienCuuCongNgheDTO> ds = dtBLL.dsDeTaiCongNghe();
            foreach(NghienCuuCongNgheDTO dt in ds )
            {
                xuatDuLieu(dt);
            }
        }

        // Cập nhập kinh phí thêm bao nhiêu %
        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            if (DanhSachRong())
            {
                MessageBox.Show("Danh sách đang rỗng hãy thêm thông tin", "Thông báo");
                return;

            }
            int i=0;
            if (cmbboxKinhPhi.Text == "5%")
            {

                DeTaiDTO.capNhapKinhPhi(1.05); 
            }
            else if (cmbboxKinhPhi.Text == "10%")
            {
                DeTaiDTO.capNhapKinhPhi(1.1);

            }
            else
            {
                MessageBox.Show("Vui lòng chọn Kinh phí hợp lệ", "Thông báo");
                return;
            }
            // tính lại kinh phí mỗi đề tài đang hiển thị
            foreach (var dt in DSDT)
            {

                dtaDanhSachDeTai.Rows[i].Cells["KinhPhi"].Value = dt.tinhKinhPhi();
                i++;
            }
            dtaDanhSachDeTai.Refresh();

        }

        private void mnuFileThoat_Click(object sender, EventArgs e)
        {
            Close();
        }


        // gợi ý thuộc tính khi mỗi lần thêm đề tài
        private void txtboxID_TextChanged(object sender, EventArgs e)
        {
           
            if(txtboxID.Text.Length>=4&&txtboxID.Text.StartsWith("NC"))
            {
                if (txtboxID.Text.Substring(2, 2) == "LT") //NCLT...
                    cmbboxThuocTinh.Text = "Áp dụng thực tế";
                else if (txtboxID.Text.Substring(2, 2) == "KT") //NCKT...
                    cmbboxThuocTinh.Text = "Số câu hỏi";
                else if (txtboxID.Text.Substring(2, 2) == "CN")//NCCN...
                    cmbboxThuocTinh.Text = "Môi trường";
            }

        }

        


       
        

        

       


        

    }
}
