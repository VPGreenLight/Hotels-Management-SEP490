import { LogOut } from "lucide-react";
import { Link, useLocation, useNavigate } from "react-router-dom";

interface MenuItem {
  name: string;
  icon: React.ElementType;
  path: string;
}

interface SidebarProps {
  menuItems?: MenuItem[];
}

const Sidebar: React.FC<SidebarProps> = ({ menuItems = [] }) => {
  const location = useLocation();
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userInfo");
    navigate("/login");
  };

  return (
    <aside className="h-screen w-64 bg-white border-r border-gray-200 flex flex-col shadow-md">
      {/* Logo */}
      <div className="flex items-center justify-center h-16 border-b border-gray-200">
        <span className="text-2xl font-semibold text-indigo-600">
         ADMINISTRATION
        </span>
      </div>

      {/* Menu */}
      <nav className="flex-1 px-4 py-4 space-y-1 overflow-y-auto">
        {menuItems.map((item) => {
          const isActive = location.pathname === item.path;
          return (
            <Link
              key={item.name}
              to={item.path}
              className={`flex items-center px-4 py-2 text-sm font-medium rounded-lg transition-colors ${
                isActive
                  ? "bg-indigo-50 text-indigo-600"
                  : "text-gray-600 hover:bg-gray-50 hover:text-indigo-600"
              }`}
            >
              <item.icon
                size={20}
                className={`mr-3 ${
                  isActive ? "text-indigo-600" : "text-gray-400"
                }`}
              />
              {item.name}
            </Link>
          );
        })}
      </nav>

      
      <div className="p-4 border-t border-gray-100">
        <button
          onClick={handleLogout}
          className="flex items-center justify-center gap-2 w-full px-4 py-2 text-sm font-medium
          text-white bg-gradient-to-r from-indigo-500 to-indigo-700
          hover:from-indigo-600 hover:to-indigo-800 active:scale-95
          rounded-xl shadow-md transition-all duration-200"
        >
          <LogOut size={18} />
          Đăng xuất
        </button>
      </div>
    </aside>
  );
};

export default Sidebar;
