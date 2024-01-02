import UserDto from "@/dto/in/user.dto";
import authApi from "@/lib/client/auth-api";
import usersApi from "@/lib/client/users-api";
import { useForm } from "@tanstack/react-form";
import { yupValidator } from "@tanstack/yup-form-adapter";
import dayjs, { Dayjs } from "dayjs";
import { useCallback, useEffect, useState } from "react";
import { toast } from "react-toastify";

type Form = ReturnType<typeof useBasicInfoFormUtils>["form"];

const defaultValues = {
  firstName: "",
  lastName: "",
  gender: "",
  dateOfBirth: null as Dayjs | null,
};

const setFormFields = (form: Form, user: Omit<UserDto, "id" | "email">) => {
  form.setFieldValue("firstName", user.firstName);
  form.setFieldValue("lastName", user.lastName);
  form.setFieldValue("gender", user.gender);
  form.setFieldValue("dateOfBirth", dayjs(user.dateOfBirth, "YYYY-MM-DD"));
};

const useBasicInfoFormUtils = () => {
  const [isLoading, setIsLoading] = useState(true);

  const form = useForm({
    defaultValues,
    validatorAdapter: yupValidator,
    onSubmit: async ({ value }) => {
      setIsLoading(true);
      try {
        await usersApi.updateUser({
          ...value,
          dateOfBirth: value.dateOfBirth?.format("YYYY-MM-DD"),
        });
        toast.success("Basic info updated successfully.");
      } catch (error) {
        toast.error("Failed to update basic info. Please try again.");
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    },
  });

  const resetForm = useCallback(async () => {
    setIsLoading(true);
    try {
      const profile = await authApi.getProfile();
      form.reset();
      setFormFields(form, profile);
    } catch (error) {
      toast.error("Failed to load basic info. Please refresh the page.");
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  }, [form]);

  useEffect(() => {
    resetForm();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return {
    form,
    state: { isLoading },
    utils: { resetForm },
  };
};

export default useBasicInfoFormUtils;
