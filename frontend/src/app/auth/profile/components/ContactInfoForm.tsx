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

  const canUpdateEmailIn = useMemo(() => {
    return state.emailUpdateOn === null
      ? 0
      : dayjs(state.emailUpdateOn)
          .add(state.defaultEmailUpdateInterval, "day")
          .diff(dayjs(), "day");
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
            >
              Reset
            </FormButton>
            <form.Subscribe selector={(s) => s.isFieldsValidating}>
              {(isValidating) => (
                <FormButton
                  type="submit"
                  variant="contained"
                  disabled={isValidating}
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
            <MuiTanTextField field={field} label="Email" type="email" />
          )}
        </form.Field>

        {!state.isLoading && (
          <Typography variant="body1">
            {canUpdateEmailIn < 1 ? (
              <>
                Once you change your email address, you will be able to change
                it again after{" "}
                <strong>{state.defaultEmailUpdateInterval} days</strong>.
              </>
            ) : (
              <>
                You will be able to change your email address after{" "}
                <strong>
                  {canUpdateEmailIn} day{canUpdateEmailIn > 1 ? "s" : ""}
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
