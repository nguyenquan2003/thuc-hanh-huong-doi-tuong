using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace QuanLyHangHoa
{
    public class HangHoa
    {
        public int Loai { get; set; }
        public string MaHH { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }

    public class Sieuthi
    {
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public List<HangHoa> DanhSachHangHoa { get; set; }

        public Sieuthi()
        {
            DanhSachHangHoa = new List<HangHoa>();
        }

        public void TaoFileXml(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();

           
            XmlNode rootNode = xmlDoc.CreateElement("SieuThi");
            xmlDoc.AppendChild(rootNode);

            
            XmlNode tenNode = xmlDoc.CreateElement("ten");
            tenNode.InnerText = Ten;
            rootNode.AppendChild(tenNode);

           
            XmlNode diaChiNode = xmlDoc.CreateElement("diachi");
            diaChiNode.InnerText = DiaChi;
            rootNode.AppendChild(diaChiNode);

           
            XmlNode hangHoasNode = xmlDoc.CreateElement("Hanghoas");
            rootNode.AppendChild(hangHoasNode);

            foreach (var hangHoa in DanhSachHangHoa)
            {
                XmlNode hangHoaNode = xmlDoc.CreateElement("Hanghoa");
                hangHoasNode.AppendChild(hangHoaNode);

                XmlNode loaiNode = xmlDoc.CreateElement("loai");
                loaiNode.InnerText = hangHoa.Loai.ToString();
                hangHoaNode.AppendChild(loaiNode);

                XmlNode maHHNode = xmlDoc.CreateElement("mahh");
                maHHNode.InnerText = hangHoa.MaHH;
                hangHoaNode.AppendChild(maHHNode);

                XmlNode soLuongNode = xmlDoc.CreateElement("soluong");
                soLuongNode.InnerText = hangHoa.SoLuong.ToString();
                hangHoaNode.AppendChild(soLuongNode);

                XmlNode donGiaNode = xmlDoc.CreateElement("dongia");
                donGiaNode.InnerText = hangHoa.DonGia.ToString();
                hangHoaNode.AppendChild(donGiaNode);
            }

           
            xmlDoc.Save(filePath);
        }

        public void DocThongTinTuFileXml(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode sieuThiNode = xmlDoc.SelectSingleNode("SieuThi");

            XmlNode tenNode = sieuThiNode.SelectSingleNode("ten");
            Ten = tenNode.InnerText;

            XmlNode diaChiNode = sieuThiNode.SelectSingleNode("diachi");
            DiaChi = diaChiNode.InnerText;

            XmlNodeList hangHoaNodes = sieuThiNode.SelectNodes("Hanghoas/Hanghoa");
            foreach (XmlNode hangHoaNode in hangHoaNodes)
            {
                HangHoa hangHoa = new HangHoa();

                XmlNode loaiNode = hangHoaNode.SelectSingleNode("loai");
                hangHoa.Loai = int.Parse(loaiNode.InnerText);

                XmlNode maHHNode = hangHoaNode.SelectSingleNode("mahh");
                hangHoa.MaHH = maHHNode.InnerText;

                XmlNode soLuongNode = hangHoaNode.SelectSingleNode("soluong");
                hangHoa.SoLuong = int.Parse(soLuongNode.InnerText);

                XmlNode donGiaNode = hangHoaNode.SelectSingleNode("dongia");
                hangHoa.DonGia = decimal.Parse(donGiaNode.InnerText);

                DanhSachHangHoa.Add(hangHoa);
            }
        }

        public decimal TongTien()
        {
            decimal tongTien = 0;
            foreach (var hangHoa in DanhSachHangHoa)
            {
                tongTien += hangHoa.SoLuong * hangHoa.DonGia;
            }
            return tongTien;
        }

        public void XuatThongTin()
        {
            Console.WriteLine("Tên Siêu thị: " + Ten);
            Console.WriteLine("Địa chỉ: " + DiaChi);
            Console.WriteLine("Danh sách hàng hóa:");
            foreach (var hangHoa in DanhSachHangHoa)
            {
                Console.WriteLine(" - Mã hàng hóa: " + hangHoa.MaHH);
                Console.WriteLine("   Số lượng: " + hangHoa.SoLuong);
                Console.WriteLine("   Đơn giá: " + hangHoa.DonGia);
            }
            Console.WriteLine("Tổng tiền: " + TongTien());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Sieuthi sieuthi = new Sieuthi();
            sieuthi.Ten = "Siêu thị ABC";
            sieuthi.DiaChi = "123 Đường X, Quận Y, Thành phố Z";

            HangHoa hangHoa1 = new HangHoa()
            {
                Loai = 1,
                MaHH = "123",
                SoLuong = 20,
                DonGia = 2400000
            };

            HangHoa hangHoa2 = new HangHoa()
            {
                Loai = 2,
                MaHH = "234",
                SoLuong = 30,
                DonGia = 220000
            };

            sieuthi.DanhSachHangHoa.Add(hangHoa1);
            sieuthi.DanhSachHangHoa.Add(hangHoa2);

            bool running = true;
            while (running)
            {
                Console.WriteLine("========== MENU ==========");
                Console.WriteLine("1. Tạo file XML lưu thông tin hàng hóa");
                Console.WriteLine("2. Đọc thông tin siêu thị từ file XML");
                Console.WriteLine("3. Tính tổng tiền các hàng hóa");
                Console.WriteLine("4. Xuất thông tin siêu thị");
                Console.WriteLine("5. Thoát");
                Console.WriteLine("==========================");

                Console.Write("Vui lòng chọn một số từ 1 đến 5: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Nhập đường dẫn tới file XML: ");
                        string filePath = Console.ReadLine();
                        sieuthi.TaoFileXml(filePath);
                        Console.WriteLine("file XML tạo thành công.");
                        break;

                    case "2":
                        Console.Write("Nhập đường dẫn tới file XML: ");
                        string filePath2 = Console.ReadLine();
                        sieuthi.DocThongTinTuFileXml(filePath2);
                        Console.WriteLine("Đọc file XML thành công.");
                        break;

                    case "3":
                        decimal tongTien = sieuthi.TongTien();
                        Console.WriteLine("Tổng tiền của các hàng hóa là: " + tongTien);
                        break;

                    case "4":
                        sieuthi.XuatThongTin();
                        break;

                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }

}
