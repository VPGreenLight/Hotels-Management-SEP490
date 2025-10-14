export const setToken = (token: string) => {
  localStorage.setItem("accesstoken", token);
};

export const getToken = () => {
  return localStorage.getItem("accesstoken");
};

export const removeToken = () => {
  localStorage.removeItem("accesstoken");
};
