"use client";
import { TextField } from "@mui/material";
import FormSection from "./FormSection";
import { FormButton } from "./styled-components";
import MuiLocalizationProvider from "@/components/MuiLocalizationProvider";
import { DatePicker } from "@mui/x-date-pickers";

const BasicInfoForm = () => (
  <FormSection
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
    <TextField label="First name" />
    <TextField label="Last name" />
    <MuiLocalizationProvider>
      <DatePicker label="Date of birth" />
    </MuiLocalizationProvider>
    <TextField label="Gender" />
  </FormSection>
);

export default BasicInfoForm;
