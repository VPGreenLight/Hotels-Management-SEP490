// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-nocheck

import {API} from "./endpoints";
import axios from "axios";
import type { AxiosRequestConfig, Method } from "axios";
import Cookies from "js-cookie";

const axiosInstance = axios.create({
  baseURL: API,
  timeout: 60000,
  withCredentials: true,
});


axiosInstance.interceptors.request.use(
  (config) => {
   const token = Cookies.get("accessToken") || localStorage.getItem("Token");

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosInstance.interceptors.response.use(
  (response) => {
    return {
      status: response?.data?.status || response?.status || 200,
      message: response?.data?.message || "Success",
      responseData: response?.data?.responseData ?? null,
    };
  },
  (error) => {
    console.log(error);
    let errorMessage = "Lỗi hệ thống";

    if (error.response) {
      const { status, data } = error.response;
      errorMessage = data?.message || errorMessage;

      return Promise.reject({
        status,
        message: errorMessage,
        responseData: null,
      });
    }

    return Promise.reject({
      status: 500,
      message: errorMessage,
      responseData: null,
    });
  }
);

export type Response<T = any> = {
  status: number;
  message: string;
  responseData: T;
};
export type LoginAdminResponse = Response<any> & {
  userInfo?: any;
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



