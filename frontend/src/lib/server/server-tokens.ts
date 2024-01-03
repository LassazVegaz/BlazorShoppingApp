import { COOKIE_NAME_TOKEN } from "@/constants";
import { cookies } from "next/headers";

export const getToken = () => {
  const cookieStore = cookies();
  const token = cookieStore.get(COOKIE_NAME_TOKEN);
  return token ? token.value : null;
};
