import { useForm } from "@tanstack/react-form";
import { formDefaultValues } from "../helpers";
import { yupValidator } from "@tanstack/yup-form-adapter";

const useSignUpUtils = () => {
  const form = useForm({
    defaultValues: formDefaultValues,
    onSubmit: (values) => {
      console.log(values);
    },
    validatorAdapter: yupValidator,
  });

  return { form };
};

export default useSignUpUtils;
