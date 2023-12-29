type CreateUser = {
  firstName: string;
  lastName: string;
  gender: string;
  dateOfBirth?: Date;
  email: string;
  password: string;
};

export default CreateUser;
