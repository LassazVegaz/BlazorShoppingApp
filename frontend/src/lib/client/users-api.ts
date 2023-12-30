import apiRoutes from "@/api-routes.json";
import clientAxios from "./client-axios";
import CreateUser from "@/dto/out/create-user";
import UpdateUser from "@/dto/out/update-user";
import UserDto from "@/dto/in/user.dto";

const emailExists = async (email: string) => {
  const response = await clientAxios.get<boolean>(
    apiRoutes.users.emailExists.replace("{email}", email)
  );
  return response.data;
};

const createUser = async (data: CreateUser) => {
  await clientAxios.post(apiRoutes.users.common, data);
};

const updateUser = async (data: UpdateUser) => {
  const res = await clientAxios.put<UserDto>(apiRoutes.users.common, data);
  return res.data;
};

const usersApi = {
  emailExists,
  createUser,
  updateUser,
};

export default usersApi;
