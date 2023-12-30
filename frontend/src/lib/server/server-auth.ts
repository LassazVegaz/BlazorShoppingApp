import serverAxios from "./server-axios";
import { getToken } from "./server-tokens";
import apiRoutes from "@/api-routes.json";

const isAuthenticated = async () => {
  const token = getToken();
  if (!token) return false;

  try {
    await serverAxios.get(apiRoutes.auth.profile);
    return true;
  } catch (error) {
    return false;
  }
};

const serverAuth = {
  isAuthenticated,
};

export default serverAuth;
