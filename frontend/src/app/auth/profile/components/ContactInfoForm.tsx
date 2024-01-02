"use client";
import { Typography } from "@mui/material";
import FormSection from "./FormSection";
import { FormButton } from "./styled-components";
import useUtils from "../hooks/contact-info-form.hook";
import dayjs from "dayjs";
import { useMemo } from "react";
import { MuiTanTextField } from "@/components/MuiTanFields";
import {
  emailValidator,
  emailValidatorAsync,
} from "@/lib/client/form-validators";

const ContactInfoForm = () => {
  const { form, state, utils } = useUtils();

  const updateEmailStatus = useMemo(() => {
    const updatedDateDiff = state.emailUpdateOn
      ? dayjs().diff(dayjs(state.emailUpdateOn), "day")
      : Infinity;
    return {
      waitingDays: state.defaultEmailUpdateInterval - updatedDateDiff,
      isAllowed: updatedDateDiff >= state.defaultEmailUpdateInterval,
    };
  }, [state.defaultEmailUpdateInterval, state.emailUpdateOn]);

  return (
    <form.Provider>
      <FormSection
        title="Contact information"
        isLoading={state.isLoading}
        onSubmit={(e) => {
          e.preventDefault();
          e.stopPropagation();
          form.handleSubmit();
        }}
        buttons={
          <>
            <FormButton
              type="reset"
              color="secondary"
              variant="contained"
              onClick={utils.resetForm}
              disabled={updateEmailStatus.isAllowed === false}
            >
              Reset
            </FormButton>
            <form.Subscribe selector={(s) => s.isFieldsValidating}>
              {(isValidating) => (
                <FormButton
                  type="submit"
                  variant="contained"
                  disabled={
                    isValidating || updateEmailStatus.isAllowed === false
                  }
                >
                  Save
                </FormButton>
              )}
            </form.Subscribe>
          </>
        }
      >
        <form.Field
          name="email"
          validators={{
            onBlur: emailValidator,
            onBlurAsyncDebounceMs: 500,
            onBlurAsync: ({ value }) => emailValidatorAsync(value),
          }}
        >
          {(field) => (
            <MuiTanTextField
              field={field}
              label="Email"
              type="email"
              disabled={updateEmailStatus.isAllowed === false}
            />
          )}
        </form.Field>

        {!state.isLoading && (
          <Typography variant="body1">
            {updateEmailStatus.isAllowed ? (
              <>
                Once you change your email address, you will be able to change
                it again after{" "}
                <strong>{state.defaultEmailUpdateInterval} days</strong>.
              </>
            ) : (
              <>
                You will be able to change your email address after{" "}
                <strong>
                  {updateEmailStatus.waitingDays} day
                  {updateEmailStatus.waitingDays > 1 ? "s" : ""}
                </strong>
                .
              </>
            )}
          </Typography>
        )}
      </FormSection>
    </form.Provider>
  );
};

export default ContactInfoForm;
