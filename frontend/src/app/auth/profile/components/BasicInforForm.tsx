"use client";
import { TextField } from "@mui/material";
import FormSection from "./FormSection";
import { FormButton } from "./styled-components";
import MuiLocalizationProvider from "@/components/MuiLocalizationProvider";
import useUtils from "../hooks/basic-info-form.hook";
import { MuiTanDateField, MuiTanTextField } from "@/components/MuiTanFields";

const BasicInfoForm = () => {
  const { form } = useUtils();

  return (
    <form.Provider>
      <FormSection
        onSubmit={(e) => {
          e.preventDefault();
          e.stopPropagation();
          form.handleSubmit();
        }}
        title="Basic information"
        buttons={
          <>
            <FormButton color="secondary" variant="contained">
              Reset
            </FormButton>
            <FormButton variant="contained">Save</FormButton>
          </>
        }
      >
        <form.Field name="firstName">
          {(field) => <MuiTanTextField field={field} label="First name" />}
        </form.Field>

        <form.Field name="lastName">
          {(field) => <MuiTanTextField field={field} label="Last name" />}
        </form.Field>

        <form.Field name="dateOfBirth">
          {(field) => (
            <MuiLocalizationProvider>
              <MuiTanDateField field={field} label="Date of birth" />
            </MuiLocalizationProvider>
          )}
        </form.Field>

        <TextField label="Gender" />
      </FormSection>
    </form.Provider>
  );
};

export default BasicInfoForm;
