import React, { useEffect, useState } from "react";
import { Home, BedDouble, Users, CalendarDays } from "lucide-react";
import Sidebar from "../../component/Sidebar";
import Navbar from "../../component/Navbar";
import Breadcrumbs from "../../component/Breadcumbs";
import Footer from "../../component/Footer";

import { request } from "../../services/api-client";
import { endpoints } from "../../services/endpoints";

const adminMenu = [
  { name: "Dashboard", icon: Home, path: "/admin/dashboard" },
  { name: "Các Cơ Sở Hiện Tại", icon: BedDouble, path: "/admin/all-branches" },
  { name: "Nhân viên", icon: Users, path: "/admin/staff" },
  { name: "Đặt phòng", icon: CalendarDays, path: "/admin/bookings" },
];

interface AdminDashboardProps {
  children: React.ReactNode;
}

const AdminDashboard: React.FC<AdminDashboardProps> = ({ children }) => {
  const [userName, setUserName] = useState<string>("Đang tải...");

  useEffect(() => {
    const fetchUser = async () => {
      try {
        // Kiểm tra localStorage
        const storedUser = localStorage.getItem("userInfo");
        if (storedUser) {
          const parsedUser = JSON.parse(storedUser);
          setUserName(parsedUser.email || "Admin");
          return;
        }

       
        const res = await request("get", endpoints.auth.me, {}, { withCredentials: true });
        if (res.status === 200 && res.responseData) {
          const data = res.responseData;
          setUserName(data.email || "Admin");
          localStorage.setItem("userInfo", JSON.stringify(data));
        } else {
          console.warn("Không thể lấy thông tin người dùng:", res);
        }
      } catch (error) {
        console.error("Lỗi khi gọi API /me:", error);
        setUserName("Admin");
      }
    };

    fetchUser();
  }, []);

  return (
    <div className="flex h-screen w-screen bg-gray-50 overflow-hidden">
      <Sidebar menuItems={adminMenu} />
      <div className="flex flex-col flex-1">
        <Navbar
          title="Bảng điều khiển quản trị"
          email={userName}
          role="Admin"
        />
        <Breadcrumbs />
        <main className="p-6 flex-1 overflow-y-auto">{children}</main>
        <Footer />
      </div>
    </div>
  );
};

export default AdminDashboard;
