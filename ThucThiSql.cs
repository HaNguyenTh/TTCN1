using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace TTCN1_QuanLyBanHangMayStore
{
    class ThucThiSql
    {
        public static SqlConnection conn;
        public static string connString = @"Data Source=HANGUYEN\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        public static void KetNoiCSDL()
        {
            conn = new SqlConnection();
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }
        public static void DongketnoiCSDL()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

        }

        public static DataTable DocBang(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter Mydata = new SqlDataAdapter();
            Mydata.SelectCommand = new SqlCommand();
            KetNoiCSDL();
            Mydata.SelectCommand.Connection = conn;
            Mydata.SelectCommand.CommandText = sql;
            Mydata.Fill(dt);
            DongketnoiCSDL();
            return dt;
        }

        //chuong trinh cap nhat du lieu
        public static void CapNhatDuLieu(string sql)
        {
            SqlCommand sqlcommand = new SqlCommand();
            KetNoiCSDL();
            sqlcommand.Connection = conn;
            sqlcommand.CommandText = sql;
            sqlcommand.ExecuteNonQuery();
            DongketnoiCSDL();
        }


        public static string GetFieldValues(string sql)
        {
            KetNoiCSDL();
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            DongketnoiCSDL();
            return ma;
        }
        //hàm tạo khóa có dạng: ngày tháng năm h phút giây
        public static string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }
        //hàm đổi định dạng từ PM sang 24h
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }




    }
}
