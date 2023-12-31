import authApi from "@/lib/client/auth-api";
import { useForm } from "@tanstack/react-form";
import { yupValidator } from "@tanstack/yup-form-adapter";
import dayjs, { Dayjs } from "dayjs";
import { useEffect, useState } from "react";

const defaultValues = {
  firstName: "",
  lastName: "",
  gender: "",
  dateOfBirth: null as Dayjs | null,
};

const useBasicInfoFormUtils = () => {
  const [isLoading, setIsLoading] = useState(true);

  const form = useForm({
    defaultValues,
    validatorAdapter: yupValidator,
  });

  useEffect(() => {
    setIsLoading(true);
    authApi
      .getProfile()
      .then((profile) => {
        form.setFieldValue("firstName", profile.firstName);
        form.setFieldValue("lastName", profile.lastName);
        form.setFieldValue("gender", profile.gender);
        form.setFieldValue(
          "dateOfBirth",
          dayjs(profile.dateOfBirth, "YYYY-MM-DD")
        );
      })
      .finally(() => setIsLoading(false));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return {
    form,
    state: { isLoading },
  };
};

export default useBasicInfoFormUtils;
