import React, { useState } from "react";
import { loginAdmin } from "../../services/authServices";
import { useNavigate } from "react-router-dom";
// import { FcGoogle } from "react-icons/fc";

const AdminLogin: React.FC = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [message, setMessage] = useState<string>("");
  // const [code, setCode] = useState<string>("");
  // const [showConfirm, setShowConfirm] = useState<boolean>(false);

  // Login bằng email/password
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const res = await loginAdmin(email, password);
      console.log(res);

      if (res?.status === 200 && res?.userInfo) {
        setMessage("Đăng nhập thành công!");
        setTimeout(() => navigate("/admin/dashboard"), 1000);
      } else {
        setMessage(res?.message || "Sai email hoặc mật khẩu!");
      }
    } catch (err) {
      console.error(err);
      setMessage("Lỗi hệ thống");
    }
  };

  // Login Google
  // const handleGoogleLogin = async () => {
  //   try {
  //     console.log("Google Login clicked");
 
  //   } catch (err) {
  //     console.error("Google login error:", err);
  //   }
  // };

  // // Send email
  // const handleSendCode = async () => {
  //   if (!email) return setMessage("Vui lòng nhập email trước!");
  //   const res = await sendConfirmationCode(email);
  //   console.log(res);
  //   setMessage(res.message || "Đã gửi mã xác nhận!");
  //   if (res.status === 200) setShowConfirm(true);
  // };

  // // Confirm email
  // const handleConfirmEmail = async () => {
  //   if (!code) return setMessage("Vui lòng nhập mã xác nhận!");
  //   const res = await confirmEmail(email, code);
  //   console.log(res);
  //   setMessage(res.message || "Email xác nhận thành công!");
  //   if (res.status === 200 && res.responseData) {
  //     setShowConfirm(false);
  //     // Redirect như login
  //     setTimeout(() => navigate("/admin/dashboard"), 1000);
  //   }
  // };

  return (
    <div className="h-screen w-screen flex">
      {/* Left side - Branding */}
      <div className="hidden lg:flex w-1/2 bg-gradient-to-br from-blue-700 via-blue-500 to-blue-300 items-center justify-center text-white">
        <div className="text-center px-10">
          <h1 className="text-5xl font-extrabold mb-6 tracking-tight drop-shadow-md">
            Hotel Management System
          </h1>
          <p className="text-lg opacity-90 leading-relaxed">
            Giải pháp toàn diện giúp quản lý chuỗi khách sạn dễ dàng, hiệu quả và hiện đại.
          </p>
        </div>
      </div>

      {/* Right side - Login form */}
      <div className="w-full lg:w-1/2 flex items-center justify-center bg-gray-50">
        <div className="w-full max-w-md p-10 bg-white rounded-2xl shadow-lg border border-gray-100">
          <h2 className="text-3xl font-bold text-center text-blue-700 mb-8">
            Đăng nhập quản trị viên
          </h2>

          <form onSubmit={handleSubmit} className="space-y-6">
            <div>
              <label className="block mb-2 font-semibold text-gray-700">Email</label>
              <input
                type="email"
                className="w-full border border-gray-300 text-gray-800 placeholder-gray-400 px-4 py-3 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400 focus:border-blue-500 transition"
                placeholder="Nhập email quản trị"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </div>
            <div>
              <label className="block mb-2 font-semibold text-gray-700">Mật khẩu</label>
              <input
                type="password"
                className="w-full border border-gray-300 text-gray-800 placeholder-gray-400 px-4 py-3 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400 focus:border-blue-500 transition"
                placeholder="Nhập mật khẩu"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>
            <button
              type="submit"
              className="w-full bg-blue-600 hover:bg-blue-700 text-white py-3 rounded-lg font-semibold text-lg shadow-md transition active:scale-[0.98]"
            >
              Đăng nhập
            </button>
          </form>

          {/* Login Google */}
          {/* <div className="flex items-center my-6">
            <hr className="flex-grow border-gray-300" />
            <span className="px-3 text-gray-400 text-sm">hoặc</span>
            <hr className="flex-grow border-gray-300" />
          </div>
          <button
            onClick={handleGoogleLogin}
            className="w-full flex items-center justify-center border border-gray-300 rounded-lg py-3 bg-white hover:bg-gray-100 transition active:scale-[0.98]"
          >
            <FcGoogle className="text-2xl mr-2" />
            <span className="text-white-700 font-medium">Đăng nhập bằng Google</span>
          </button> */}

          {/* Gửi mã & xác nhận email */}
          {/* {email && (
            <div className="mt-6 space-y-3">
              <button
                onClick={handleSendCode}
                className="w-full bg-yellow-500 hover:bg-yellow-600 text-white py-2 rounded-lg"
              >
                Gửi mã xác nhận
              </button>

              {showConfirm && (
                <>
                  <input
                    type="text"
                    className="w-full border px-4 py-2 rounded-lg"
                    placeholder="Nhập mã xác nhận"
                    value={code}
                    onChange={(e) => setCode(e.target.value)}
                  />
                  <button
                    onClick={handleConfirmEmail}
                    className="w-full bg-green-600 hover:bg-green-700 text-white py-2 rounded-lg"
                  >
                    Xác nhận email
                  </button>
                </>
              )}
            </div>
          )} */}

          {/* Message */}
          {message && (
            <p
              className={`mt-6 text-center font-medium ${message.includes("thành công") ? "text-green-600" : "text-red-600"}`}
            >
              {message}
            </p>
          )}

          {/* Footer */}
          <div className="mt-10 text-center text-sm text-gray-500">
            © {new Date().getFullYear()} Tân Trường Sơn Hotel Management System
          </div>
        </div>
      </div>
    </div>
  );
};

export default AdminLogin;
