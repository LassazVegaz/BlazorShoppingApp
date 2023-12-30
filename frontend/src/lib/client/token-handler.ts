const tokenKey = "token";

const getToken = () => {
  return localStorage.getItem(tokenKey);
};

const setToken = (token: string) => {
  localStorage.setItem(tokenKey, token);
};

const removeToken = () => {
  localStorage.removeItem(tokenKey);
};

const tokenHandler = {
  getToken,
  setToken,
  removeToken,
};

export default tokenHandler;
