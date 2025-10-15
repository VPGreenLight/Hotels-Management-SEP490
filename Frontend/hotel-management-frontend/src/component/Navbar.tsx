import React from "react";
import { UserCircle } from "lucide-react";

interface NavbarProps {
  title?: string;
  email?: string; 
  role?: string;
  onProfileClick?: () => void;
  onLogout?: () => void;
}

const Navbar: React.FC<NavbarProps> = ({
  title = "Quản lý khách sạn",
  email = "Admin",
  role = "",
 
}) => {
  return (
    <div className="h-16 bg-white border-b border-gray-200 px-6 flex items-center justify-between shadow-sm">
  
      <div className="font-semibold text-gray-700 text-lg">{title}</div>

   
      <div className="flex items-center gap-6">

     
        <div className="flex items-center gap-3">
          <UserCircle size={30} className="text-gray-600" />
          <div className="flex flex-col leading-tight text-gray-700">
            <span className="font-medium">{email}</span>
            {role && (
              <span className="text-xs text-gray-500 capitalize">{role}</span>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
