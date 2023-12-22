import AuthContainer from "@/components/AuthContainer";
import { FormsFieldsContainer } from "@/components/AuthContainer/styled-components";
import { Button, Stack, TextField } from "@mui/material";

const SignInPage = () => {
  return (
    <AuthContainer
      leftPanel={{
        title: "Welcome Back!",
        subtitle: "Still don't have an account?",
        buttonText: "Sign Up",
        buttonLink: "/auth/signup",
      }}
    >
      <FormsFieldsContainer>
        <TextField label="Email" type="email" size="small" />
        <TextField label="Password" type="password" size="small" />
      </FormsFieldsContainer>

      <Stack alignItems="center" mt={5}>
        <Button variant="contained">Sign In</Button>
      </Stack>
    </AuthContainer>
  );
};

export default SignInPage;
