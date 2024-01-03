import CreateUser from "./create-user";

type UpdateUser = Partial<Omit<CreateUser, "password">>;

export default UpdateUser;
