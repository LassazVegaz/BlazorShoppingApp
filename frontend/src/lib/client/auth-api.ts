import apiRoutes from "@/api-routes.json";
import axios from "./axios";
import UserDto from "@/dto/in/user.dto";

const login = async (email: string, password: string) => {
  const response = await axios.post<string>(apiRoutes.auth.login, {
    email,
    password,
  });
  return response.data;
};

const getProfile = async () => {
  const response = await axios.get<UserDto>(apiRoutes.auth.profile);
  return response.data;
};

const authApi = {
  login,
  getProfile,
};

export default authApi;
