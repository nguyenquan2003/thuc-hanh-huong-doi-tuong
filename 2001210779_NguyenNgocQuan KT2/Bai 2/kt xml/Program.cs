using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace BankingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument xmlDoc = XDocument.Load("../../data.xml");
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1. Xuất thông tin ngân hàng");
                Console.WriteLine("2. Tính tổng điểm thưởng của khách hàng VIP");
                Console.WriteLine("3. Hiển thị thông tin khách hàng có điểm thưởng cao nhất");
                Console.WriteLine("4. Tính tổng giá trị giao dịch của khách hàng tiềm năng");
                Console.WriteLine("5. In ra thông tin chi tiết của tất cả các khách hàng");
                Console.WriteLine("6. Sắp xếp danh sách khách hàng theo họ tên và điểm thưởng");
                Console.WriteLine("7. Tìm và hiển thị thông tin khách hàng theo mã");
                Console.WriteLine("0. Thoát chương trình");

                Console.Write("Vui lòng chọn: ");
                int choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        XuatThongTinNganHang(xmlDoc);
                        break;
                    case 2:
                        TinhTongDiemThuongKHVIP(xmlDoc);
                        break;
                    case 3:
                        HienThiKHCoDiemThuongCaoNhat(xmlDoc);
                        break;
                    case 4:
                        TinhTongGiaTriGiaoDichKHTiemNang(xmlDoc);
                        break;
                    case 5:
                        InThongTinChiTietTatCaKH(xmlDoc);
                        break;
                    case 6:
                        SapXepDSKHTangDan(xmlDoc);
                        break;
                    case 7:
                        TimVaHienThiThongTinKHTheoMa(xmlDoc);
                        break;
                    case 0:
                        Console.WriteLine("Đã thoát chương trình.");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void XuatThongTinNganHang(XDocument xmlDoc)
        {
            string tenChiNhanh = xmlDoc.Element("ChiNhanh").Element("TenChiNhanh").Value;
            string diaChi = xmlDoc.Element("ChiNhanh").Element("DiaChi").Value;

            Console.WriteLine("Thông tin ngân hàng:");
            Console.WriteLine("Tên chi nhánh: " + tenChiNhanh);
            Console.WriteLine("Địa chỉ: " + diaChi);
        }

        static void TinhTongDiemThuongKHVIP(XDocument xmlDoc)
        {
            var khachHangVIPs = xmlDoc.Descendants("KH").Where(kh => kh.Attribute("Loai").Value == "VIP");

            int tongDiemThuong = 0;
            foreach (var khachHangVIP in khachHangVIPs)
            {
                int diemThuong = int.Parse(khachHangVIP.Element("ThoiHan").Value);
                tongDiemThuong += diemThuong;
            }

            Console.WriteLine("Tổng điểm thưởng của khách hàng VIP: " + tongDiemThuong);
        }

        static void HienThiKHCoDiemThuongCaoNhat(XDocument xmlDoc)
        {
            var khachHangVIPs = xmlDoc.Descendants("KH").Where(kh => kh.Attribute("Loai").Value == "VIP");

            var khachHangCaoNhat = khachHangVIPs.OrderByDescending(kh => int.Parse(kh.Element("ThoiHan").Value)).FirstOrDefault();

            if (khachHangCaoNhat != null)
            {
                string maKhachHangCaoNhat = khachHangCaoNhat.Element("MA").Value;
                string tenKhachHangCaoNhat = khachHangCaoNhat.Element("TEN").Value;
                int diemThuongCaoNhat = int.Parse(khachHangCaoNhat.Element("ThoiHan").Value);

                Console.WriteLine("Thông tin khách hàng có điểm thưởng cao nhất:");
                Console.WriteLine("Mã khách hàng: " + maKhachHangCaoNhat);
                Console.WriteLine("Tên khách hàng: " + tenKhachHangCaoNhat);
                Console.WriteLine("Điểm thưởng: " + diemThuongCaoNhat);
            }
            else
            {
                Console.WriteLine("Không có khách hàng VIP nào.");
            }
        }

        static void TinhTongGiaTriGiaoDichKHTiemNang(XDocument xmlDoc)
        {
            var khachHangTiemNang = xmlDoc.Descendants("KH").Where(kh => kh.Attribute("Loai").Value == "TN");

            int tongGiaTriGiaoDich = 0;
            foreach (var khachHang in khachHangTiemNang)
            {
                int giaTriGiaoDich = int.Parse(khachHang.Element("TongGiaTri").Value);
                tongGiaTriGiaoDich += giaTriGiaoDich;
            }

            Console.WriteLine("Tổng giá trị giao dịch của khách hàng tiềm năng: " + tongGiaTriGiaoDich);
        }

        static void InThongTinChiTietTatCaKH(XDocument xmlDoc)
        {
            var khachHangs = xmlDoc.Descendants("KH");

            Console.WriteLine("Thông tin chi tiết của tất cả các khách hàng:");

            foreach (var khachHang in khachHangs)
            {
                string maKhachHang = khachHang.Element("MA").Value;
                string tenKhachHang = khachHang.Element("TEN").Value;
                string gioiTinh = khachHang.Element("GT").Value;

                Console.WriteLine("Mã khách hàng: " + maKhachHang);
                Console.WriteLine("Tên khách hàng: " + tenKhachHang);
                Console.WriteLine("Giới tính: " + gioiTinh);
                Console.WriteLine();
            }
        }

        static void SapXepDSKHTangDan(XDocument xmlDoc)
        {
            var khachHangs = xmlDoc.Descendants("KH");

            var khachHangsSapXep = khachHangs.OrderBy(kh => kh.Element("TEN").Value)
                                             .ThenByDescending(kh => int.Parse(kh.Element("ThoiHan").Value));

            Console.WriteLine("Danh sách khách hàng sau khi sắp xếp:");

            foreach (var khachHang in khachHangsSapXep)
            {
                string maKhachHang = khachHang.Element("MA").Value;
                string tenKhachHang = khachHang.Element("TEN").Value;
                string diemThuong = khachHang.Element("ThoiHan").Value;

                Console.WriteLine("Mã khách hàng: " + maKhachHang);
                Console.WriteLine("Tên khách hàng: " + tenKhachHang);
                Console.WriteLine("Điểm thưởng: " + diemThuong);
                Console.WriteLine();
            }
        }

        static void TimVaHienThiThongTinKHTheoMa(XDocument xmlDoc)
        {
            Console.Write("Nhập mã khách hàng: ");
            string maKhachHangTimKiem = Console.ReadLine();

            var khachHangTimKiem = xmlDoc.Descendants("KH").FirstOrDefault(kh => kh.Element("MA").Value == maKhachHangTimKiem);

            if (khachHangTimKiem != null)
            {
                string maKhachHang = khachHangTimKiem.Element("MA").Value;
                string tenKhachHang = khachHangTimKiem.Element("TEN").Value;
                string gioiTinh = khachHangTimKiem.Element("GT").Value;

                Console.WriteLine("Thông tin khách hàng có mã " + maKhachHangTimKiem + ":");
                Console.WriteLine("Mã khách hàng: " + maKhachHang);
                Console.WriteLine("Tên khách hàng: " + tenKhachHang);
                Console.WriteLine("Giới tính: " + gioiTinh);
            }
            else
            {
                Console.WriteLine("Khách hàng mới");
            }
        }
    }
}
