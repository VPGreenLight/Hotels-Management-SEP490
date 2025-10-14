import { useNavigate } from "react-router-dom";
import background from "../../assets/backgroung.jpg";

const Login = () => {
  const navigate = useNavigate();

  return (
    <div
      className="h-screen w-screen flex items-center justify-center bg-gradient-to-br from-blue-400 via-blue-200 to-white relative overflow-hidden"
      style={{ backgroundImage: `url(${background})`, backgroundSize: "cover" }}
    >
      {/* Lớp mờ overlay */}
      <div className="absolute inset-0 bg-white/50 backdrop-blur-sm"></div>

      {/* Form chọn loại đăng nhập */}
      <div className="relative z-10 w-[90%] max-w-2xl bg-white/90 backdrop-blur-md rounded-3xl shadow-2xl p-10 border border-white/40">
        <h1 className="text-4xl font-bold text-blue-700 mb-8 text-center">
          Đăng nhập hệ thống quản lý chuỗi khách sạn Tân Trường Sơn
        </h1>

        <form className="flex flex-col gap-5 items-center">
          <button
            type="button"
            onClick={() => navigate("/admin-login")}
            className="w-full bg-blue-600 hover:bg-blue-700 text-white text-lg font-semibold py-3 rounded-xl shadow-md hover:shadow-lg transition"
          >
            Đăng nhập với quyền Admin
          </button>

          <button
            type="button"
            onClick={() => navigate("/staff-login")}
            className="w-full bg-green-600 hover:bg-green-700 text-white text-lg font-semibold py-3 rounded-xl shadow-md hover:shadow-lg transition"
          >
            Đăng nhập với quyền Staff/Manager
          </button>
        </form>
      </div>
    </div>
  );
};

export default Login;
