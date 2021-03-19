using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySach.ReportViewer
{

    public partial class frm_bien_lai : Form
    {
        SqlConnection ketnoi = new SqlConnection();
        string so_hd = "";
        public frm_bien_lai(string sohd)
        {
            InitializeComponent();
            ketnoi.ConnectionString = Properties.Settings.Default.ChuoiKetnoi;
            ketnoi.Open();
            this.so_hd = sohd;
        }

        private void frm_bien_lai_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            LoadBill(so_hd);
            this.reportViewer1.RefreshReport();
        }


        private void LoadBill(string sohd)
        {
            try
            {
                String caulenh = "select hd.SoHD , CONVERT(varchar, hd.NgayBan, 103) as NgayBan ," +
                    " hd.GioBan,hd.ThuNgan , s.TenSach , ct.SoLuongBan , hd.SLHang , ct.GiaBan ," +
                    " ct.GiamGia,hd.TongTien , hd.KhachDua , ct.MaKH ," +
                    "(ct.SoLuongBan * ct.GiaBan - ct.SoLuongBan * ct.GiaBan * ct.GiamGia / 100) as ThanhTien ," +
                    "(hd.KhachDua - hd.TongTien) as TienThua from HoaDon hd, ChiTietHoaDon ct, Sach s" +
                    " where ct.SoHD = hd.SoHD and s.MaSach = ct.MaSach and hd.SoHD = "+sohd;
                SqlDataAdapter sqldata = new SqlDataAdapter(caulenh, ketnoi);
                DataSet1 ds = new DataSet1();
                sqldata.Fill(ds, "Data_bien_lai");
                ReportDataSource reportDataSource = new ReportDataSource("DataSet_Report" , ds.Tables[0]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                this.reportViewer1.RefreshReport();



            }
            catch
            {

            }
        }
    }
}
