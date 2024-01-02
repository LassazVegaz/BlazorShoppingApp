import { TextField, Typography } from "@mui/material";
import FormSection from "./FormSection";
import { FormButton } from "./styled-components";

const ContactInfoForm = () => {
  return (
    <FormSection
      title="Contact information"
      buttons={
        <>
          <FormButton color="secondary" variant="contained">
            Reset
          </FormButton>
          <FormButton variant="contained">Save</FormButton>
        </>
      }
    >
      <TextField label="Email" type="email" />

      <Typography variant="body1">
        You can change your email address only once every 30 days.
      </Typography>
    </FormSection>
  );
};

export default ContactInfoForm;
