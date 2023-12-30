import authApi from "@/lib/client/auth-api";
import { useForm } from "@tanstack/react-form";
import dayjs, { Dayjs } from "dayjs";
import { useEffect } from "react";

const defaultValues = {
  firstName: "",
  lastName: "",
  gender: "",
  dateOfBirth: null as Dayjs | null,
};

const useBasicInfoFormUtils = () => {
  const form = useForm({
    defaultValues,
  });

  useEffect(() => {
    authApi.getProfile().then((profile) => {
      form.setFieldValue("firstName", profile.firstName);
      form.setFieldValue("lastName", profile.lastName);
      form.setFieldValue("gender", profile.gender);
      form.setFieldValue(
        "dateOfBirth",
        dayjs(profile.dateOfBirth, "YYYY-MM-DD")
      );
    });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return { form };
};

export default useBasicInfoFormUtils;
