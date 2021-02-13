using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTCN1_QuanLyBanHangMayStore
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmNV f = new Forms.FrmNV();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmNCC f = new Forms.FrmNCC();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();

        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmKH f = new Forms.FrmKH();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void loạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmLSP f = new Forms.FrmLSP();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmSP f = new Forms.FrmSP();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();

        }

        private void hóaĐơnBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmHDBH f = new Forms.FrmHDBH();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void phiếuNhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmPNH f = new Forms.FrmPNH();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void sảnPhẩmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Forms.FrmTKSP f = new Forms.FrmTKSP();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void kháchHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Forms.FrmTKKH f = new Forms.FrmTKKH();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void hóaĐơnBánHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Forms.FrmTKHDBH f = new Forms.FrmTKHDBH();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void phiếuNhậpHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Forms.FrmTKPNH f = new Forms.FrmTKPNH();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FrmBC f = new Forms.FrmBC();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
