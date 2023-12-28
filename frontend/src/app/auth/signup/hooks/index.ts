import { useForm } from "@tanstack/react-form";
import { formDefaultValues } from "../helpers";

const useSignUpUtils = () => {
  const form = useForm({
    defaultValues: formDefaultValues,
    onSubmit: (values) => {
      console.log(values);
    },
  });

  return { form };
};

export default useSignUpUtils;
