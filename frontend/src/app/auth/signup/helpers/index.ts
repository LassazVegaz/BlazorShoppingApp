import * as Yup from "yup";
import { Dayjs } from "dayjs";

const genderValues = ["male", "female", "other"] as const;

export const formDefaultValues = {
  firstName: "",
  lastName: "",
  email: "",
  gender: "",
  password: "",
  passwordConfirmation: "",
  dateOfBirth: null as Dayjs | null,
};

export const validators = {
  firstName: Yup.string().required("First name is required"),
  lastName: Yup.string().required("Last name is required"),
  email: Yup.string().email("Email is invalid").required("Email is required"),
  gender: Yup.string().oneOf(genderValues, "Select a gender"),
  password: Yup.string()
    .required("Required")
    .matches(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[a-zA-Z\d@$!%*?&]{8,}$/,
      "Password must be at least 8 characters long and contain at least one number, one uppercase letter, one lowercase letter and one special character from @$!%*?&"
    ),
  /**
   * @param password Value of password field
   * @returns Yup validation schema for password confirmation field
   */
  passwordConfirmation: (password: string) =>
    Yup.string().required("Required").oneOf([password], "Passwords must match"),
  dateOfBirth: Yup.date().required("Required"),
};

const makeFirstLetterUppercase = (str: string) =>
  str[0].toUpperCase() + str.slice(1);

/**
 * Use to generate Gender field's dropdown options
 */
export const genderDropdowns = genderValues.map(
  (value) =>
    ({
      label: makeFirstLetterUppercase(value),
      value,
    } as const)
);
