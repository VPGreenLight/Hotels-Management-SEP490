import { request, type LoginAdminResponse } from "./api-client";
import { API, endpoints } from "./endpoints";

export const login = async (email: string, password: string) => {
  const res = await request("post", `${API}${endpoints.auth.login}`, {
    loginEmail: email,
    password,
  },
    {
      withCredentials: true,
    });
  if (res.status) {
    const userInfo = await request("get", `${API}${endpoints.auth.me}`, {}, { withCredentials: true });
    if (userInfo.status) {
      localStorage.setItem("userInfo", JSON.stringify(userInfo.responseData));
    }
  }
  return res;
};
export const loginAdmin = async (email: string, password: string): Promise<LoginAdminResponse> => {
  const res = await request("post", endpoints.auth.adminLogin, {
    loginEmail: email,
    password,
  }, { withCredentials: true });
  if (res.status === 200) {
    const userInfo = await request("get", endpoints.auth.me, {}, { withCredentials: true });
    if (userInfo.status === 200) {
      localStorage.setItem("userInfo", JSON.stringify(userInfo.responseData));
      return { ...res, userInfo: userInfo.responseData };
    }
  }
  return res;
};
//send Email
export const sendConfirmationCode = async (email: string) => {
  const res = await request(
    "post",
    endpoints.auth.sendEmail, 
    { email },
    { withCredentials: true }
  );
  if (res.status === 200) {
    return { ...res };
  }

  return res;
};
//confirm Email
export const confirmEmail = async (email: string, code: string) => {
  const res = await request(
    "post",
    endpoints.auth.confirmEmail, 
    { email, code },
    { withCredentials: true }
  );
  if (res.status === 200 && res.responseData) { 
    localStorage.setItem("userInfo", JSON.stringify(res.responseData));
  }
  return res;
};