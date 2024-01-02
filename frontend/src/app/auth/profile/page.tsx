import { Container, Stack, Typography } from "@mui/material";
import {
  BasicInfoForm,
  ContactInfoForm,
  LogoutButton,
  PasswordForm,
} from "./components";
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

        <PasswordForm />
      </Stack>

      <LogoutButton />
    </Container>
  );
};

export default SignUpPage;
