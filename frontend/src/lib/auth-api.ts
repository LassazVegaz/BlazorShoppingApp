import apiRoutes from "@/api-routes.json";
import axios from "./axios";

const login = async (email: string, password: string) => {
  const response = await axios.post<string>(apiRoutes.auth.login, {
    email,
    password,
  });
  return response.data;
};

const authApi = {
  login,
};

export default authApi;
