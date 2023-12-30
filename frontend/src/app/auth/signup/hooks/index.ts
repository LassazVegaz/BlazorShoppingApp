import { useForm } from "@tanstack/react-form";
import { formDefaultValues } from "../helpers";
import { yupValidator } from "@tanstack/yup-form-adapter";
import { useAppDispatch } from "@/redux/hooks";
import { pageLoaderActions } from "@/redux/slices/page-loader.slice";
import usersApi from "@/lib/client/users-api";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";

const useSignUpUtils = () => {
  const dispatch = useAppDispatch();
  const router = useRouter();

  const form = useForm({
    defaultValues: formDefaultValues,
    validatorAdapter: yupValidator,
    onSubmit: async ({ value }) => {
      dispatch(pageLoaderActions.setLoading(true));
      try {
        const { passwordConfirmation: _, dateOfBirth, ...rest } = value;
        await usersApi.createUser({
          ...rest,
          dateOfBirth: dateOfBirth!.format("YYYY-MM-DD"),
        });
        toast.success("Account created successfully");
        router.push("/auth/signin");
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
