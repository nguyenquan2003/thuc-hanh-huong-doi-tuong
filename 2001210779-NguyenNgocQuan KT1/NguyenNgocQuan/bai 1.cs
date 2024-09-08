using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace NguyenNgocQuan
{
    class HangHoa
    {
        private string maHangHoa;
        private string tenHangHoa;
        private int soLuong;
        private double donGia;
        private string loaiHangHoa;
        public string MaHangHoa
        {
            get { return maHangHoa; }
            set
            {
                if (value.StartsWith("HH") && value.Length == 4)
                    maHangHoa = value;
                else
                    maHangHoa = "HH01";
            }
        }
        public string TenHangHoa
        {
            get { return tenHangHoa; }
            set { tenHangHoa = value; }
        }
        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        public double DonGia
        {
            get { return donGia; }
            set { donGia = value; }
        }
        public string LoaiHangHoa
        {
            get { return loaiHangHoa; }
            set { loaiHangHoa = value; }
        }
        public HangHoa()
        {
            MaHangHoa = "HH01";
            TenHangHoa = "Tivi";
            SoLuong = 10;
            DonGia = 1000000;
            LoaiHangHoa = "Gia dung";
        }
        public HangHoa(string ma, string ten, int sl, double gia, string loai)
        {
            MaHangHoa = ma;
            TenHangHoa = ten;
            SoLuong = sl;
            DonGia = gia;
            LoaiHangHoa = loai;
        }
        public double TinhThanhTien()
        {
            return SoLuong * DonGia;
        }
        public void XuatThongTin()
        {
            Console.WriteLine("Ma hang hoa: " + MaHangHoa);
            Console.WriteLine("Ten hang hoa: " + TenHangHoa);
            Console.WriteLine("So luong: " + SoLuong);
            Console.WriteLine("Don gia: " + DonGia);
            Console.WriteLine("Loai hang hoa: " + LoaiHangHoa);
        }
        class Program
        {
            static void Main(string[] args)
            {
                HangHoa hangHoa = new HangHoa();
                bool exit = false;
                Console.OutputEncoding = Encoding.UTF8;
                while (!exit)
                {
                    Console.WriteLine("----- MENU -----");
                    Console.WriteLine("1. Nhập thông tin hàng hóa");
                    Console.WriteLine("2. Xuất thông tin hàng hóa");
                    Console.WriteLine("3. Tính thành tiền");
                    Console.WriteLine("4. Khởi tạo hàng hóa mặc định");
                    Console.WriteLine("5. Khởi tạo hàng hóa từ thông tin có sẵn");
                    Console.WriteLine("6. Thoát");
                    Console.Write("Chọn chức năng từ 1-6: ");
                    string menu = Console.ReadLine();

                    switch (menu)
                    {
                        case "1":
                          
                            Console.Write("Nhập mã hàng hóa: ");
                            hangHoa.MaHangHoa = Console.ReadLine();

                            Console.Write("Nhập tên hàng hóa: ");
                            hangHoa.TenHangHoa = Console.ReadLine();

                            Console.Write("Nhập số lượng hàng hóa: ");
                            hangHoa.SoLuong = int.Parse(Console.ReadLine());

                            Console.Write("Nhập đơn giá hàng hóa: ");
                            hangHoa.DonGia = double.Parse(Console.ReadLine());

                            Console.Write("Nhập loại hàng hóa: ");
                            hangHoa.LoaiHangHoa = Console.ReadLine();
                            break;

                        case "2":
                           
                            Console.WriteLine("--- Thông tin hàng hóa ---");
                            hangHoa.XuatThongTin();
                            Console.WriteLine("--------------------------");
                            break;

                        case "3":
                           
                            double thanhTien = hangHoa.TinhThanhTien();
                            Console.WriteLine("Thành tiền: " + thanhTien);
                            break;

                        case "4":
                           
                            hangHoa = new HangHoa();
                            Console.WriteLine("Đã khởi tạo hàng hóa mặc định");
                            break;

                        case "5":
     
                            Console.Write("Nhập mã hàng hóa: ");
                            string maHangHoa = Console.ReadLine();

                            Console.Write("Nhập tên hàng hóa: ");
                            string tenHangHoa = Console.ReadLine();

                            Console.Write("Nhập số lượng hàng hóa: ");
                            int soLuong = int.Parse(Console.ReadLine());

                            Console.Write("Nhập đơn giá hàng hóa: ");
                            double donGia = double.Parse(Console.ReadLine());

                            Console.Write("Nhập loại hàng hóa: ");
                            string loaiHangHoa = Console.ReadLine();

                            hangHoa = new HangHoa(maHangHoa, tenHangHoa, soLuong, donGia, loaiHangHoa);
                            Console.WriteLine("Đã khởi tạo hàng hóa từ thông tin có sẵn");
                            break;

                        case "6":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Chọn không hợp lệ. Vui lòng chọn lại.");
                            break;
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
