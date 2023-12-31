"use client";
import FormSection from "./FormSection";
import { FormButton } from "./styled-components";
import MuiLocalizationProvider from "@/components/MuiLocalizationProvider";
import useUtils from "../hooks/basic-info-form.hook";
import { MuiTanDateField, MuiTanTextField } from "@/components/MuiTanFields";
import GenderDropdown from "./GenderDropdown";

const BasicInfoForm = () => {
  const { form, state } = useUtils();

  return (
    <form.Provider>
      <FormSection
        isLoading={state.isLoading}
        title="Basic information"
        onSubmit={(e) => {
          e.preventDefault();
          e.stopPropagation();
          form.handleSubmit();
        }}
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

        <form.Field name="gender">
          {(field) => <GenderDropdown field={field} />}
        </form.Field>
      </FormSection>
    </form.Provider>
  );
};

export default BasicInfoForm;
