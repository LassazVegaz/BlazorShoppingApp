import _axios from "axios";
import clientTokens from "./client-tokens";

const clientAxios = _axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
  },
});

clientAxios.interceptors.request.use(
  (config) => {
    const token = clientTokens.getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default clientAxios;
