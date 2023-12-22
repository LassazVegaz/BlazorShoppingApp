import { Container, Stack, TextField, Typography } from "@mui/material/index";
import { BasicInfoForm, FormSection } from "./components";
import { FormButton } from "./components/styled-components";

const SignUpPage = () => {
  return (
    <Container sx={{ pb: 5, pt: 1 }}>
      <Typography variant="h1" fontSize={45} textAlign="center">
        Manage your profile here
      </Typography>

      <Stack mt={7} spacing={10}>
        <BasicInfoForm />

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

        <FormSection
          title="Password"
          buttons={<FormButton variant="contained">Save</FormButton>}
        >
          <TextField label="Current password" type="password" />
          <TextField label="Password" type="password" />
          <TextField label="Confirm password" type="password" />
        </FormSection>
      </Stack>
    </Container>
  );
};

export default SignUpPage;
