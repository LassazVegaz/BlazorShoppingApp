import apiRoutes from "@/api-routes.json";
import clientAxios from "./client-axios";
import CreateUser from "@/dto/out/create-user";

const emailExists = async (email: string) => {
  const response = await clientAxios.get<boolean>(
    apiRoutes.users.emailExists.replace("{email}", email)
  );
  return response.data;
};

const createUser = async (data: CreateUser) => {
  await clientAxios.post(apiRoutes.users.common, data);
};

const usersApi = {
  emailExists,
  createUser,
};

export default usersApi;
