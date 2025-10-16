import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import background from "../../assets/backgroung.jpg";
import { login } from "../../services/authServices"; 

const StaffLogin: React.FC = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [showPassword, setShowPassword] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(false);

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);

    try {
      const res = await login(email, password);

      if (res.status === 200) {
        
        const userInfo = JSON.parse(localStorage.getItem("userInfo") || "{}");

        if (userInfo && userInfo.roles?.length > 0) {
          const role = userInfo.roles[0];

          if (role === "Branch Manager") {
            navigate("/branchmanager/dashboard");
          } else if (role === "Housekeeping Staff") {
            navigate("/housekeeping/dashboard");
          } else if (role === "Receptionist") {
            navigate("/receptionist/dashboard");
          } else {
            alert("Không xác định được vai trò của tài khoản!");
          }
        } else {
          alert("Không tìm thấy thông tin người dùng!");
        }
      } else {
        alert("Đăng nhập thất bại! Vui lòng kiểm tra lại thông tin.");
      }
    } catch (error) {
      console.error("Login error:", error);
      alert("Đã xảy ra lỗi trong quá trình đăng nhập!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div
      className="h-screen w-screen flex items-center justify-center bg-gradient-to-br from-green-300 via-white to-green-100 relative overflow-hidden"
      style={{ backgroundImage: `url(${background})`, backgroundSize: "cover" }}
    >
      {/* Overlay mờ */}
      <div className="absolute inset-0 bg-white/60 backdrop-blur-sm"></div>

      {/* Form đăng nhập */}
      <div className="relative z-10 w-[90%] max-w-md bg-white/90 backdrop-blur-md rounded-3xl shadow-2xl p-10 border border-white/40">
        <h1 className="text-3xl font-bold text-green-700 mb-2 text-center">
          Đăng nhập Nhân viên / Quản lý
        </h1>
        <p className="text-gray-600 mb-8 text-center">
          Hệ thống quản lý chuỗi khách sạn Tân Trường Sơn
        </p>

        <form onSubmit={handleLogin} className="flex flex-col gap-5">
          <div>
            <label className="block text-gray-700 font-medium mb-1">Email</label>
            <input
              type="email"
              placeholder="Nhập email của bạn"
              className="w-full bg-white text-gray-900 border border-gray-300 rounded-lg p-3 
                        focus:outline-none focus:ring-2 focus:ring-green-400 placeholder-gray-400"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>

          <div>
            <label className="block text-gray-700 font-medium mb-1">Mật khẩu</label>
            <div className="relative">
              <input
                type={showPassword ? "text" : "password"}
                placeholder="Nhập mật khẩu"
                className="w-full bg-white text-gray-900 border border-gray-300 rounded-lg p-3 
                          focus:outline-none focus:ring-2 focus:ring-green-400 pr-10 placeholder-gray-400"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute inset-y-0 right-3 flex items-center text-gray-500 hover:text-green-600"
              >
                {showPassword ? "🙈" : "👁️"}
              </button>
            </div>
          </div>

          <button
            type="submit"
            disabled={loading}
            className={`w-full ${
              loading ? "bg-green-400" : "bg-green-600 hover:bg-green-700"
            } text-white text-lg font-semibold py-3 rounded-xl shadow-md hover:shadow-lg transition`}
          >
            {loading ? "Đang đăng nhập..." : "Đăng nhập"}
          </button>

          <p className="text-center text-sm text-gray-600">
            Quay lại{" "}
            <span
              onClick={() => navigate("/login")}
              className="text-green-600 hover:underline cursor-pointer"
            >
              chọn loại tài khoản
            </span>
          </p>
        </form>
      </div>
    </div>
  );
};

export default StaffLogin;
