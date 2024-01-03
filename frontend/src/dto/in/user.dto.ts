type UserDto = {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  gender: string;
  dateOfBirth: string;
  emailUpdatedOn?: string;
};

export default UserDto;
