"use client";
import FormSection from "./FormSection";
import { FormButton } from "./styled-components";
import useUtils from "../hooks/password-form.hook";
import { MuiTanTextField } from "@/components/MuiTanFields";
import * as Yup from "yup";
import {
  passwordConfirmationValidator,
  passwordValidator,
} from "@/lib/client/form-validators";
import { ValidationError } from "yup";

const PasswordForm = () => {
  const { form, state } = useUtils();

  return (
    <form.Provider>
      <FormSection
        isLoading={state.isLoading}
        onSubmit={(e) => {
          e.preventDefault();
          e.stopPropagation();
          form.handleSubmit();
        }}
        onReset={form.reset}
        title="Password"
        buttons={
          <form.Subscribe selector={(s) => s.isSubmitting}>
            {(isSubmitting) => (
              <>
                <FormButton
                  type="reset"
                  variant="contained"
                  disabled={isSubmitting}
                  color="secondary"
                >
                  Reset
                </FormButton>
                <FormButton
                  type="submit"
                  variant="contained"
                  disabled={isSubmitting}
                >
                  Save
                </FormButton>
              </>
            )}
          </form.Subscribe>
        }
      >
        <form.Field
          name="oldPassword"
          validators={{
            onBlur: Yup.string().required("Required"),
          }}
        >
          {(field) => (
            <MuiTanTextField
              field={field}
              label="Current password"
              type="password"
            />
          )}
        </form.Field>

        <form.Field
          name="newPassword"
          validators={{
            onBlur: passwordValidator,
          }}
        >
          {(field) => (
            <MuiTanTextField field={field} label="Password" type="password" />
          )}
        </form.Field>

        <form.Field
          name="confirmPassword"
          validators={{
            onBlur: (data) => {
              try {
                const password =
                  data.fieldApi.form.getFieldValue("newPassword");
                passwordConfirmationValidator(password).validateSync(
                  data.value
                );
              } catch (error) {
                if (error instanceof ValidationError) return error.message;
                throw error;
              }
            },
          }}
        >
          {(field) => (
            <MuiTanTextField
              field={field}
              label="Confirm password"
              type="password"
            />
          )}
        </form.Field>
      </FormSection>
    </form.Provider>
  );
};

export default PasswordForm;
