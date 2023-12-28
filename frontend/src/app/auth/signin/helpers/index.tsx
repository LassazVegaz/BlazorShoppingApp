import * as Yup from "yup";

export type FormValues = typeof formDefaultValues;

export const formDefaultValues = {
  email: "",
  password: "",
};

export const validators = {
  email: Yup.string().email("Email is invalid").required("Required"),
  password: Yup.string()
    .required("Required")
    .matches(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[a-zA-Z\d@$!%*?&]{8,}$/,
      "Password must be at least 8 characters long and contain at least one number, one uppercase letter, one lowercase letter and one special character from @$!%*?&"
    ),
};
