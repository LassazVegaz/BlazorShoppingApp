import apiRoutes from "@/api-routes.json";
import clientAxios from "./client-axios";
import UserDto from "@/dto/in/user.dto";

const login = async (email: string, password: string) => {
  const response = await clientAxios.post<string>(apiRoutes.auth.login, {
    email,
    password,
  });
  return response.data;
};

const getProfile = async () => {
  const response = await clientAxios.get<UserDto>(apiRoutes.auth.profile);
  return response.data;
};

const authApi = {
  login,
  getProfile,
};

export default authApi;
