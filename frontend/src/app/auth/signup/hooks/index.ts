import { useForm } from "@tanstack/react-form";
import { formDefaultValues } from "../helpers";
import { yupValidator } from "@tanstack/yup-form-adapter";
import { useAppDispatch } from "@/redux/hooks";
import { pageLoaderActions } from "@/redux/slices/page-loader.slice";
import usersApi from "@/lib/users-api";
import { toast } from "react-toastify";

const useSignUpUtils = () => {
  const dispatch = useAppDispatch();

  const form = useForm({
    defaultValues: formDefaultValues,
    validatorAdapter: yupValidator,
    onSubmit: async ({ value }) => {
      dispatch(pageLoaderActions.setLoading(true));
      try {
        const { passwordConfirmation: _, dateOfBirth, ...rest } = value;
        await usersApi.createUser({
          ...rest,
          dateOfBirth: dateOfBirth!.toDate(),
        });
        toast.success("Account created successfully");
      } catch (error) {
        toast.error("Sorry, something went wrong. Please try again later.");
        console.error(error);
      } finally {
        dispatch(pageLoaderActions.setLoading(false));
      }
    },
  });

  return { form };
};

export default useSignUpUtils;
