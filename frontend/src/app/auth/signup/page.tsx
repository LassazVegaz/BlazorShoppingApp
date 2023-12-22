import AuthContainer from "@/components/AuthContainer";
import { FormsFieldsContainer } from "@/components/AuthContainer/styled-components";
import { Button, Stack, TextField } from "@mui/material/index";

const SignUpPage = () => {
  return (
    <AuthContainer
      leftPanel={{
        title: "Welcome!",
        subtitle: "Already have an account?",
        buttonText: "Sign In",
        buttonLink: "/auth/signin",
      }}
    >
      <FormsFieldsContainer>
        <TextField label="First name" size="small" />
        <TextField label="Last name" size="small" />
        <TextField label="Email" type="email" size="small" />
        <TextField label="Date of birth" type="date" size="small" />
        <TextField label="Gender" size="small" />
        <TextField label="Password" type="password" size="small" />
        <TextField label="Confirm password" type="password" size="small" />
      </FormsFieldsContainer>

      <Stack alignItems="center" mt={5}>
        <Button variant="contained">Sign Up</Button>
      </Stack>
    </AuthContainer>
  );
};

export default SignUpPage;
