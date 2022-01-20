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

namespace WindowsFormsApp1
{
    
    public partial class add : Form
    {
        public ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
        public string url = "Data source=LAPTOP_PE_NAFI; database=data_pasien;User ID=sa;Password=123";
        public add()
        {
            InitializeComponent();
            btn_clear.Enabled = false;
            btn_clear_up.Enabled = false;
            btn_Clear_del.Enabled = false;
            tampil();
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            string id_pasien = tb_ID.Text;
            string NIK = tb_nik.Text;
            string nama_pasien = tb_nama.Text;
            string agama = cb_agama.Text;
            string jns_kelamin = cb_jnskelamin.Text;
            string tempat_lahir = tb_tmplahir.Text;
            string tgl_lahir = datelahir.Text;
            string no_telpon = tb_notlp.Text;
            string alamat = tb_alamat.Text;
           
            var a = service.insertdata_pasien(id_pasien, NIK, nama_pasien, tempat_lahir, tgl_lahir, agama, jns_kelamin, alamat, no_telpon);
            MessageBox.Show(a);
            btn_clear.Enabled = true;
            tampil();
        }
        public void clear()
        {
            tampil();
            //tambah data
            tb_ID.Text = "";
            tb_nik.Text = "";
            tb_nama.Text = "";
            cb_agama.Text = "";
            cb_jnskelamin.Text = "";
            tb_tmplahir.Text = "";
            datelahir.Text = "";
            tb_notlp.Text = "";
            tb_alamat.Text = "";
            // update data
            tb_cariID_up.Text = "";
            tb_nikup.Text = "";
            tb_namaup.Text = "";
            cb_agama_up.Text = "";
            cb_jnsklm_up.Text = "";
            tb_tmplahirup.Text = "";
            datelahir_up.Text = "";
            tb_notlp_up.Text = "";
            tb_almt_up.Text = "";
            //delete data
            tb_cariID_del.Text = "";
            tb_nik_del.Text = "";
            tb_nama_del.Text = "";
            //batton
            btn_clear.Enabled = false;
            btn_clear_up.Enabled = false;
            btn_Clear_del.Enabled = false;
            btn_save.Enabled = true;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            string id_pasien = tb_cariID_up.Text;
            string NIK = tb_nikup.Text;
            string nama_pasien = tb_namaup.Text;
            string agama = cb_agama_up.Text;
            string jns_kelamin = cb_jnsklm_up.Text;
            string tempat_lahir = tb_tmplahirup.Text;
            string tgl_lahir = datelahir_up.Text;
            string no_telpon = tb_notlp_up.Text;
            string alamat = tb_almt_up.Text;

            var a = service.editdata_pasien(id_pasien, NIK, nama_pasien, tempat_lahir, tgl_lahir, agama, jns_kelamin, alamat, no_telpon);
            MessageBox.Show(a);
            btn_clear_up.Enabled = true;
            tampil();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string id_pasien = tb_cariID_del.Text;

            var a = service.deletedata_pasien(id_pasien);
            MessageBox.Show(a);
            btn_Clear_del.Enabled = true;
            tampil();
        }

        private void btn_cariID_UP_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(url);
            con.Open();
            SqlCommand cm = new SqlCommand("select  [NIK], [nama_pasien], [tempat_lahir], [tanggal_lahir],[agama],[jenis_kelamin],[no_telpon],[alamat] " +
                                                            "from[dbo].[data_pasien] where[id_pasien] = '" + tb_cariID_up.Text + "'", con);
            var r = cm.ExecuteReader();
            if (r.Read())
            {
                tb_nikup.Text = r[0].ToString();
                tb_namaup.Text = r[1].ToString();
                tb_tmplahirup.Text = r[2].ToString();
                datelahir_up.Text = r[3].ToString();
                cb_agama_up.Text = r[4].ToString();
                cb_jnsklm_up.Text = r[5].ToString();
                tb_notlp_up.Text = r[6].ToString();
                tb_almt_up.Text = r[7].ToString();
            }
            else
            {
                MessageBox.Show("No record found with this id", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
        }   

        public void tampil()
        {
            var read = service.listdata_pasien();
            viewdata.DataSource = read;
            
        }

        private void btn_clear_up_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_Clear_del_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_cariID_del_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(url);
            con.Open();
            SqlCommand cm = new SqlCommand("select  [NIK], [nama_pasien] " +
                                                            "from[dbo].[data_pasien] where[id_pasien] = '" + tb_cariID_del.Text + "'", con);
            var r = cm.ExecuteReader();
            if (r.Read())
            {
                tb_nik_del.Text = r[0].ToString();
                tb_nama_del.Text = r[1].ToString();
            }
            else
            {
                //menampilkan pemberiathuan
                MessageBox.Show("No record found with this id", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // menutup koneksi
            con.Close();
        }
    }
}
