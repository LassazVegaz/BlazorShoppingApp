import { useForm } from "@tanstack/react-form";
import { yupValidator } from "@tanstack/yup-form-adapter";
import { formDefaultValues } from "../helpers";

const useSignInUtils = () => {
  const form = useForm({
    defaultValues: formDefaultValues,
    onSubmit: (data) => {
      console.log(data);
    },
    validatorAdapter: yupValidator,
  });

  return { form };
};

export default useSignInUtils;
