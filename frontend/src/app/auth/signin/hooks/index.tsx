import { useForm } from "@tanstack/react-form";
import { yupValidator } from "@tanstack/yup-form-adapter";
import { formDefaultValues } from "../helpers";
import { useAppDispatch } from "@/redux/hooks";
import { pageLoaderActions } from "@/redux/slices/page-loader.slice";
import authApi from "@/lib/auth-api";
import { AxiosError } from "axios";
import { toast } from "react-toastify";

const useSignInUtils = () => {
  const dispatch = useAppDispatch();

  const form = useForm({
    defaultValues: formDefaultValues,
    validatorAdapter: yupValidator,
    onSubmit: async ({ value }) => {
      dispatch(pageLoaderActions.setLoading(true));
      try {
        const token = await authApi.login(value.email, value.password);
        console.log("token:", token);
      } catch (error) {
        if (error instanceof AxiosError && error.status === 401) {
          toast.error("Invalid credentials");
        } else {
          toast.error("Something went wrong. Please try again later.");
          console.error(error);
        }
      } finally {
        dispatch(pageLoaderActions.setLoading(false));
      }
    },
  });

  return { form };
};

export default useSignInUtils;
