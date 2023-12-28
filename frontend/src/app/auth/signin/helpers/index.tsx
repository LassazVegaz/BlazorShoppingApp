import * as Yup from "yup";

export type FormValues = typeof formDefaultValues;

export const formDefaultValues = {
  email: "",
  password: "",
};

export const validators = {
  email: Yup.string().email("Email is invalid").required("Required"),
  password: Yup.string().required("Required"),
};
