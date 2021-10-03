using ASSIGNMENT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
namespace ASSIGNMENT
{
    public partial class Form1 : Form
    {
        //ServiceDanhBa _serviceDB = new ServiceDanhBa();
        ISeviceDanhBa _IseviceDanhBa;


        public Form1()
        {
            InitializeComponent();
            _IseviceDanhBa = new ServiceDanhBa();
            loaddata();
        }
        void loaddata()
        {
            foreach (var x in _IseviceDanhBa.getYearOfBirth())
            {
                cmb_namsinh.Items.Add(x);
            }
            cmb_namsinh.SelectedIndex = _IseviceDanhBa.getYearOfBirth().Length - 10;

            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.getlstnguoi())
            {
                drg_view.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh, x.GioiTinh == true ? "Nam" : "Nu", _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Email).FirstOrDefault(),
                     _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt1).FirstOrDefault(),
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt2).FirstOrDefault(),
                      _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.GhiChu).FirstOrDefault());
            }

        }

        private void btn_add_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc muốn them không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DanhBa danhBa = new DanhBa();
                Nguoi nguoi = new Nguoi();
                nguoi.Ho = txt_ho.Text;
                nguoi.TenDem = txt_tendem.Text;
                nguoi.Ten = txt_ten.Text;
                nguoi.NamSinh = Convert.ToInt32(cmb_namsinh.SelectedItem);
                nguoi.GioiTinh = rbt_nam.Checked;
                danhBa.Email = txt_email.Text;
                danhBa.Sdt1 = txt_sdt1.Text;
                danhBa.Sdt2 = txt_sdt2.Text;
                danhBa.GhiChu = txt_ghichu.Text;
                MessageBox.Show(_IseviceDanhBa.Add(nguoi, danhBa), "thong bao");
                _IseviceDanhBa.getlistfromdb();
                loaddata();
                return;
            }



        }

        private void drg_view_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex == _IseviceDanhBa.getlstnguoi().Count) return;
            txt_ho.ReadOnly = true;
            txt_tendem.ReadOnly = true;
            txt_ten.ReadOnly = true;
            txt_ho.Text = drg_view.Rows[rowindex].Cells[0].Value.ToString();
            txt_tendem.Text = drg_view.Rows[rowindex].Cells[1].Value.ToString();
            txt_ten.Text = drg_view.Rows[rowindex].Cells[2].Value.ToString();
            txtten_enable.Text = drg_view.Rows[rowindex].Cells[2].Value.ToString();
            cmb_namsinh.SelectedItem = drg_view.Rows[rowindex].Cells[3].Value.ToString();

            if (drg_view.Rows[rowindex].Cells[4].Value.ToString() == "Nam")
            {
                rbt_nam.Checked = true;

            }
            else
            {
                rbt_nu.Checked = true;
            }
            txt_email.Text = drg_view.Rows[rowindex].Cells[5].Value.ToString();
            txt_sdt1.Text = drg_view.Rows[rowindex].Cells[6].Value.ToString();
            txt_sdt2.Text = drg_view.Rows[rowindex].Cells[7].Value.ToString();
            txt_ghichu.Text = drg_view.Rows[rowindex].Cells[8].Value.ToString();

        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            DialogResult xacnhan = MessageBox.Show("ban co muon xoa", "thong bao", MessageBoxButtons.YesNo);
            if (xacnhan == DialogResult.Yes)
            {
                MessageBox.Show(_IseviceDanhBa.Del(_IseviceDanhBa.getlstnguoi().Where(c => c.Ten == txt_ten.Text).Select(c => c.Id).FirstOrDefault()), "thong bao");
                _IseviceDanhBa.getlistfromdb();
                loaddata();
                return;
            }
           
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            DialogResult xacnhan = MessageBox.Show("ban co muon sua", "thong bao", MessageBoxButtons.YesNo);
            if (xacnhan == DialogResult.Yes)
            {
                Nguoi nguoi = new Nguoi();
                DanhBa danhBa = new DanhBa();
                nguoi = _IseviceDanhBa.getlstnguoi().Where(c => c.Ten == txt_ten.Text).FirstOrDefault();
                nguoi.NamSinh = Convert.ToInt32(cmb_namsinh.SelectedItem);
                nguoi.GioiTinh = rbt_nam.Checked;
                danhBa = _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == nguoi.Id).FirstOrDefault();
                danhBa.Email = txt_email.Text;
                danhBa.Sdt1 = txt_sdt1.Text;
                danhBa.Sdt2 = txt_sdt2.Text;
                danhBa.GhiChu = txt_ghichu.Text;
                MessageBox.Show(_IseviceDanhBa.update(danhBa, nguoi), "thong bao");
                loaddata();
                return;
            }
           




        }

        private void Form1_Load(object sender, EventArgs e)
        {




        }

        private void cl_clear_Click(object sender, EventArgs e)
        {
            txt_ho.ReadOnly = false;
            txt_tendem.ReadOnly = false;
            txt_ten.ReadOnly = false;
            txt_ho.ResetText();
            txt_tendem.ResetText();
            txt_ten.ResetText();
            cmb_namsinh.SelectedItem = "2021";
            txt_email.ResetText();
            txt_sdt1.ResetText();
            txt_sdt2.ResetText();
            txt_ghichu.ResetText();
            loaddata();
        }


        void loaddatasearchsdt(string sdt)
        {


            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.Search1(sdt))
            {
                drg_view.Rows.Add(_IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.Ho).FirstOrDefault(),
                   _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.TenDem).FirstOrDefault(),
                  _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.Ten).FirstOrDefault(),
                  _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.NamSinh).FirstOrDefault(),
                  _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.GioiTinh == true ? "Nam" : "Nu").FirstOrDefault(),
                     x.Email, x.Sdt1, x.Sdt2, x.GhiChu);
            }

        }

        private void txt_searchSdt_TextChanged(object sender, EventArgs e)
        {
            loaddatasearchsdt(txt_searchSdt.Text);
        }

        private void txt_SearchTen_TextChanged(object sender, EventArgs e)
        {
            loaddatasearchten(txt_SearchTen.Text);
        }

        void loaddatasearchten(string ten)
        {
            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.Search(ten))
            {
                drg_view.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh, x.GioiTinh == true ? "Nam" : "Nu",
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Email).FirstOrDefault(),
                     _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt1).FirstOrDefault(),
                   _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt2).FirstOrDefault(),
                      _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.GhiChu).FirstOrDefault());
            }
        }

        private void btn_loctheobcc_Click(object sender, EventArgs e)
        {
            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.LocTBCC())
            {
                drg_view.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh, x.GioiTinh == true ? "Nam" : "Nu",
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Email).FirstOrDefault(),
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt1).FirstOrDefault(),
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt2).FirstOrDefault(),
                      _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.GhiChu).FirstOrDefault());
            }
        }

        private void btn_loctheoten_Click(object sender, EventArgs e)
        {

        }


        void locten(string ten)
        {
            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.LocTTen(ten))
            {
                drg_view.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh, x.GioiTinh == true ? "Nam" : "Nu",
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Email).FirstOrDefault(),
                     _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt1).FirstOrDefault(),
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt2).FirstOrDefault(),
                      _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.GhiChu).FirstOrDefault());
            }
        }

        private void btn_nam_Click(object sender, EventArgs e)
        {
            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.LocTGT())
            {
                drg_view.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh, x.GioiTinh == true ? "Nam" : "Nu",
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Email).FirstOrDefault(),
                     _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt1).FirstOrDefault(),
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt2).FirstOrDefault(),
                      _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.GhiChu).FirstOrDefault());
            }
        }

        private void btn_nu_Click(object sender, EventArgs e)
        {
            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.LocTGT1())
            {
                drg_view.Rows.Add(x.Ho, x.TenDem, x.Ten, x.NamSinh, x.GioiTinh == true ? "Nam" : "Nu",
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Email).FirstOrDefault(),
                     _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt1).FirstOrDefault(),
                    _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.Sdt2).FirstOrDefault(),
                      _IseviceDanhBa.getlstdanhba().Where(c => c.IdNguoi == x.Id).Select(c => c.GhiChu).FirstOrDefault());
            }
        }

        private void btn_loctheovt_Click(object sender, EventArgs e)
        {
            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.NhamangViettel())
            {
                drg_view.Rows.Add(_IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.Ho).FirstOrDefault(),
                   _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.TenDem).FirstOrDefault(),
                  _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.Ten).FirstOrDefault(),
                  _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.NamSinh).FirstOrDefault(),
                  _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.GioiTinh == true ? "Nam" : "Nu").FirstOrDefault(),
                     x.Email, x.Sdt1, x.Sdt2, x.GhiChu);
            }
        }

        private void btn_loctheovina_Click(object sender, EventArgs e)
        {
            drg_view.ColumnCount = 9;
            drg_view.Columns[0].Name = "Họ";
            drg_view.Columns[1].Name = "Tên đệm";
            drg_view.Columns[2].Name = "Tên";
            drg_view.Columns[3].Name = "Năm sinh1";
            drg_view.Columns[4].Name = "Giới tính";
            drg_view.Columns[5].Name = "Email";
            drg_view.Columns[6].Name = "Sdt1";
            drg_view.Columns[7].Name = "Sdt2";
            drg_view.Columns[8].Name = "Ghi chú";
            drg_view.Rows.Clear();
            foreach (var x in _IseviceDanhBa.NhamangVina())
            {
                drg_view.Rows.Add(_IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.Ho).FirstOrDefault(),
                   _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.TenDem).FirstOrDefault(),
                 _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.Ten).FirstOrDefault(),
                  _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.NamSinh).FirstOrDefault(),
                  _IseviceDanhBa.getlstnguoi().Where(c => c.Id == x.IdNguoi).Select(c => c.GioiTinh == true ? "Nam" : "Nu").FirstOrDefault(),
                     x.Email, x.Sdt1, x.Sdt2, x.GhiChu);
            }
        }

        private void cl_loctheoten_Click(object sender, EventArgs e)
        {
            locten(txt_ten.Text);
        }
    }
}
