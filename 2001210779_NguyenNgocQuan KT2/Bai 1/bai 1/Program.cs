using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace _KT
{
    abstract class Khachhang
    {
        protected string MaKH { get; set; }
        protected string HoTen { get; set; }
        protected string Gioitinh { get; set; }
        protected double Diemthuong { get; set; }

        public Khachhang()
        {
            //phthu khoi tao mac dinh
        }

        public Khachhang(string Ma, string HT, string Gt, double Dt)
        {
            MaKH = Ma;
            HoTen = HT;
            Gioitinh = Gt;
            Diemthuong = Dt;
        }

        public virtual void Input()
        {
            Console.Write("Nhập mã khách hàng: ");
            MaKH = Console.ReadLine();
            Console.Write("Nhập họ tên khách hàng: ");
            HoTen = Console.ReadLine();
            Console.Write("Nhập giới tính khách hàng: ");
            Gioitinh = Console.ReadLine();
            Console.Write("Nhập điểm thưởng khách hàng:");
            Diemthuong = double.Parse(Console.ReadLine());
        }

        public virtual void Display()
        {
            Console.WriteLine("Mã khách hàng: " + MaKH);
            Console.WriteLine("Họ tên khách hàng: " + HoTen);
            Console.WriteLine("Giới tính khách hàng: " + Gioitinh);
            Console.WriteLine("Điểm thưởng: " + Diemthuong);
        }
    }

    class KHTN : Khachhang
    {
        public double TongGiaTriGiaoDich { get; set; }

        public override void Input()
        {
            base.Input();
            Console.Write("Nhập tổng giá trị giao dịch: ");
            TongGiaTriGiaoDich = double.Parse(Console.ReadLine());
        }

        public override void Display()
        {
            Console.WriteLine("===== Khách hàng tiềm năng =====");
            base.Display();
            Console.WriteLine("Tổng giá trị giao dịch: " + TongGiaTriGiaoDich);
        }

        public void TinhDiemThuong()
        {
            Diemthuong = TongGiaTriGiaoDich * 0.01;
        }
    }

    class KHTT : Khachhang
    {
        public int StartYear { get; set; }

        public override void Input()
        {
            base.Input();
            Console.Write("Nhập năm bắt đầu tham gia: ");
            StartYear = int.Parse(Console.ReadLine());
        }

        public override void Display()
        {
            Console.WriteLine("===== Khách hàng thân thiết =====");
            base.Display();
            Console.WriteLine("Năm bắt đầu tham gia: " + StartYear);
            Console.WriteLine("===============================");
        }

        public void TinhDiemThuong()
        {
            Diemthuong = (DateTime.Now.Year - StartYear) * 3650;
        }
    }

    class VIPCustomer : Khachhang
    {
        public double GiaTriTaiSan { get; set; }
        public int ThoiHanGui { get; set; }

        public override void Input()
        {
            base.Input();
            Console.Write("Nhập giá trị tài sản: ");
            GiaTriTaiSan = double.Parse(Console.ReadLine());
            Console.Write("Nhập thời hạn gửi (tháng): ");
            ThoiHanGui = int.Parse(Console.ReadLine());
        }

        public override void Display()
        {
            Console.WriteLine("===== Khách hàng VIP =====");
            base.Display();
            Console.WriteLine("Giá trị tài sản: " + GiaTriTaiSan);
            Console.WriteLine("Thời hạn gửi: " + ThoiHanGui);
            Console.WriteLine("=========================");
        }

        public void TinhDiemThuong()
        {
            double heSo = 0;
            if (ThoiHanGui < 6)
                heSo = 0.2;
            else if (ThoiHanGui >= 6 && ThoiHanGui <= 12)
                heSo = 0.5;
            else if (ThoiHanGui > 12)
                heSo = 1;

            Diemthuong = GiaTriTaiSan * 0.1 * heSo;
        }
    }

    class CustomerManager
    {
        public string BranchName { get; set; }
        public string Address { get; set; }
        public Khachhang[] Customers { get; set; }

        public CustomerManager()
        {
        }

        public CustomerManager(string branchName, string address)
        {
            BranchName = branchName;
            Address = address;
        }

        public void InputCustomers()
        {
            Console.Write("Nhập số lượng khách hàng: ");
            int customerCount = int.Parse(Console.ReadLine());
            Customers = new Khachhang[customerCount];

            for (int i = 0; i < customerCount; i++)
            {
                Console.WriteLine("Nhập thông tin khách hàng thứ " + (i + 1));
                Console.Write("Chọn loại khách hàng (1: Tiềm năng, 2: Thân thiết, 3: VIP): ");
                int customerType = int.Parse(Console.ReadLine());

                switch (customerType)
                {
                    case 1:
                        KHTN potentialCustomer = new KHTN();
                        potentialCustomer.Input();
                        potentialCustomer.TinhDiemThuong();
                        Customers[i] = potentialCustomer;
                        break;
                    case 2:
                        KHTT loyalCustomer = new KHTT();
                        loyalCustomer.Input();
                        loyalCustomer.TinhDiemThuong();
                        Customers[i] = loyalCustomer;
                        break;
                    case 3:
                        VIPCustomer vipCustomer = new VIPCustomer();
                        vipCustomer.Input();
                        vipCustomer.TinhDiemThuong();
                        Customers[i] = vipCustomer;
                        break;
                    default:
                        Console.WriteLine("Loại khách hàng không hợp lệ!");
                        i--;
                        break;
                }
            }
        }

        public void DisplayCustomers()
        {
            Console.WriteLine("===== Thông tin khách hàng tại chi nhánh " + BranchName + " =====");

            foreach (Khachhang customer in Customers)
            {
                customer.Display();
            }

            Console.WriteLine("========================================");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager manager = new CustomerManager("Chi nhánh A", "123 ABC Street");
            bool exit = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (!exit)
            {
                Console.WriteLine("===== MENU =====");
                Console.WriteLine("1. Nhập thông tin khách hàng");
                Console.WriteLine("2. Hiển thị thông tin khách hàng");
                Console.WriteLine("3. Thoát");
                Console.WriteLine("================");

                Console.Write("Nhập lựa chọn của bạn: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("===== NHẬP THÔNG TIN KHÁCH HÀNG =====");
                        manager.InputCustomers();
                        Console.WriteLine("=====================================");
                        break;
                    case "2":
                        Console.WriteLine("===== HIỂN THỊ THÔNG TIN KHÁCH HÀNG =====");
                        manager.DisplayCustomers();
                        Console.WriteLine("========================================");
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
