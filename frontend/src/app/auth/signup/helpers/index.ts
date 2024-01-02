import * as Yup from "yup";
import { Dayjs } from "dayjs";
import {
  emailValidator,
  emailValidatorAsync,
  firstNameValidator,
  genderValidator,
  lastNameValidator,
  passwordConfirmationValidator,
  passwordValidator,
} from "@/lib/client/form-validators";

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
  firstName: firstNameValidator,
  lastName: lastNameValidator,
  email: emailValidator,
  emailAsync: emailValidatorAsync,
  gender: genderValidator,
  password: passwordValidator,
  /**
   * @param password Value of password field
   * @returns Yup validation schema for password confirmation field
   */
  passwordConfirmation: passwordConfirmationValidator,
  dateOfBirth: Yup.date().required("Required"),
};
