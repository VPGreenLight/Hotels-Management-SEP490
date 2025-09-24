
import axios, { AxiosRequestConfig, AxiosResponse, Method } from "axios";
//import Cookies from "js-cookie";

const axiosInstance = axios.create({
  timeout: 60000,
}); 

// axiosInstance.interceptors.request.use(
//   (config) => {
//     // const token = localStorage.getItem("user_token");
//     // if (token) {
//     //   config.headers.Authorization = `Bearer ${token}`;
//     // }
//     const token = Cookies.get("Token");
//     if (token) {
//       config.headers.Authorization = `Bearer ${token}`;
//     }
//     return config;
//   },
//   (error) => {
//     return Promise.reject(error);
//   }
// );

axiosInstance.interceptors.response.use(
  (response) => {
    let status: boolean = false;
    if (response?.status === 200 || response?.status === 201) {
      status = true;
    }

    return {
        ...response,
        data: {
            status: status,
            message: response?.data?.message || "success",
            result: response.data,
        }
      
    };
  },
  (error) => {
    console.log(error);
    let errorMessage = "System error";

    if (error?.message?.includes("System error")) {
      errorMessage = "System error";
    } else if (error.response) {
      const { status, data } = error.response;

      if (status === 401) {
        // window.localStorage.clear();
        // window.location.href = "/login";
        return Promise.reject({
          status: false,
          message: "Account without permission",
          result: null,
        });
      } else if (status === 400) {
        errorMessage = data?.message;

        if (errorMessage === "Account access expired") {
          window.localStorage.clear();
        }

        return Promise.reject({
          status: false,
          message: errorMessage,
          result: null,
        });
      } else if (status === 404 || status === 502) {
        errorMessage = data?.message || "System error";
        return Promise.reject({
          status: false,
          message: errorMessage,
          result: null,
        });
      }
    }
    return Promise.reject(error);
  }
);

export type Response<T = any> = {
  status: boolean;
  message: string;
  result: T;
};

export type MyResponse<T = any> = Promise<Response<T>>;

/**
 *
 * @param method
 * @param url
 * @param data
 */

export const request = <T = any>(
  method: Lowercase<Method>,
  url: string,
  data?: any,
  config?: AxiosRequestConfig,
): MyResponse<T> => {
  const prefix = "";

  url = prefix + url;

  if (method === "post") {
    return axiosInstance.post(url, data, config);
  } else if (method === "delete") {
    return axiosInstance.delete(url, config);
  } else if (method === "put") {
    return axiosInstance.put(url, data, config);
  } else {
    return axiosInstance.get(url, {
      params: data,
      ...config,
    });
  }
};



