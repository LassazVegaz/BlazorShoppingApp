import usersApi from "@/lib/client/users-api";
import { useForm } from "@tanstack/react-form";
import { yupValidator } from "@tanstack/yup-form-adapter";
import { useState } from "react";
import { toast } from "react-toastify";

const defaultValues = {
  oldPassword: "",
  newPassword: "",
  confirmPassword: "",
};

const usePasswordFormUtils = () => {
  const [isLoading, setIsLoading] = useState(false);

  const form = useForm({
    defaultValues,
    validatorAdapter: yupValidator,
    onSubmit: async ({ value }) => {
      setIsLoading(true);
      try {
        await usersApi.changePassword({
          oldPassword: value.oldPassword,
          newPassword: value.newPassword,
        });
        toast.success("Password updated successfully.");
      } catch (error) {
        toast.error("Failed to update password. Please try again.");
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    },
  });

  return {
    form,
    state: { isLoading },
  };
};

export default usePasswordFormUtils;
