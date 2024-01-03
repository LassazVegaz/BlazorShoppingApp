import { API_DATE_FORMAT } from "@/constants";
import UserDto from "@/dto/in/user.dto";
import authApi from "@/lib/client/auth-api";
import usersApi from "@/lib/client/users-api";
import { useForm } from "@tanstack/react-form";
import { yupValidator } from "@tanstack/yup-form-adapter";
import dayjs from "dayjs";
import { useCallback, useEffect, useRef, useState } from "react";
import { toast } from "react-toastify";

type Form = ReturnType<typeof useContactInfoFormUtils>["form"];

const defaultValues = {
  email: "",
};

const setFormFields = (form: Form, user: Pick<UserDto, "email">) => {
  form.setFieldValue("email", user.email);
};

const useContactInfoFormUtils = () => {
  const emailUpdateInterval = useRef(0);
  const [isLoading, setIsLoading] = useState(true);
  const [emailUpdateOn, setEmailUpdateOn] = useState<null | Date>(null);

  const form = useForm({
    defaultValues,
    validatorAdapter: yupValidator,
    onSubmit: async ({ value }) => {
      setIsLoading(true);
      try {
        const user = await usersApi.updateUser(value);
        _setEmailUpdateOn(user);

        toast.success("Contact info updated successfully.");
      } catch (error) {
        toast.error("Failed to update contact info. Please try again.");
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    },
  });

  const _setEmailUpdateOn = useCallback(
    (user: Pick<UserDto, "emailUpdatedOn">) => {
      if (user.emailUpdatedOn) {
        setEmailUpdateOn(dayjs(user.emailUpdatedOn, API_DATE_FORMAT).toDate());
      } else {
        setEmailUpdateOn(null);
      }
    },
    []
  );

  const resetForm = useCallback(async () => {
    setIsLoading(true);
    try {
      const profile = await authApi.getProfile();
      form.reset();
      setFormFields(form, profile);
      _setEmailUpdateOn(profile);
    } catch (error) {
      toast.error("Failed to load contact info. Please refresh the page.");
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  }, [_setEmailUpdateOn, form]);

  const initData = useCallback(async () => {
    setIsLoading(true);
    try {
      const userOptions = await usersApi.getUserOptions();
      emailUpdateInterval.current = userOptions.emailUpdateIntervalInDays;

      const profile = await authApi.getProfile();
      setFormFields(form, profile);
      _setEmailUpdateOn(profile);
    } catch (error) {
      toast.error("Failed to load contact info. Please refresh the page.");
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  }, [_setEmailUpdateOn, form]);

  useEffect(() => {
    initData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return {
    form,
    state: {
      isLoading,
      emailUpdateOn,
      defaultEmailUpdateInterval: emailUpdateInterval.current,
    },
    utils: { resetForm },
  };
};

export default useContactInfoFormUtils;
