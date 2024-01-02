import * as Yup from "yup";
import usersApi from "./users-api";
import { genderValues } from "./form-constants";

export const firstNameValidator = Yup.string().required(
  "First name is required"
);

export const lastNameValidator = Yup.string().required("Last name is required");

export const emailValidator = Yup.string()
  .email("Email is invalid")
  .required("Email is required");

export const emailValidatorAsync = async (email: string) => {
  try {
    const emailTaken = await usersApi.emailExists(email);
    if (emailTaken) return "Email is already taken";
  } catch (error) {
    return "Cannot validate email at this time";
  }
};

export const genderValidator = Yup.string().oneOf(
  genderValues,
  "Select a gender"
);

export const passwordValidator = Yup.string()
  .required("Required")
  .matches(
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[a-zA-Z\d@$!%*?&]{8,}$/,
    "Password must be at least 8 characters long and contain at least one number, one uppercase letter, one lowercase letter and one special character from @$!%*?&"
  );

export const passwordConfirmationValidator = (password: string) =>
  Yup.string().required("Required").oneOf([password], "Passwords must match");
