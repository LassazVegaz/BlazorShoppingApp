import { Container, Stack, TextField, Typography } from "@mui/material";
import {
  BasicInfoForm,
  ContactInfoForm,
  FormSection,
  LogoutButton,
} from "./components";
import { FormButton } from "./components/styled-components";
import serverAuth from "@/lib/server/server-auth";
import { redirect } from "next/navigation";

const SignUpPage = async () => {
  if (!(await serverAuth.isAuthenticated())) redirect("/auth/signin");

  return (
    <Container sx={{ pb: 5, pt: 1 }}>
      <Typography variant="h1" fontSize={45} textAlign="center">
        Manage your profile here
      </Typography>

      <Stack mt={7} spacing={10}>
        <BasicInfoForm />

        <ContactInfoForm />

        <FormSection
          title="Password"
          buttons={<FormButton variant="contained">Save</FormButton>}
        >
          <TextField label="Current password" type="password" />
          <TextField label="Password" type="password" />
          <TextField label="Confirm password" type="password" />
        </FormSection>
      </Stack>

      <LogoutButton />
    </Container>
  );
};

export default SignUpPage;
