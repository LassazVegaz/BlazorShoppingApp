"use client";
import AuthContainer from "@/components/AuthContainer";
import { FormsFieldsContainer } from "@/components/AuthContainer/styled-components";
import MuiLocalizationProvider from "@/components/MuiLocalizationProvider";
import { Button, Stack, TextField } from "@mui/material/index";
import { DatePicker } from "@mui/x-date-pickers";

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
        <MuiLocalizationProvider>
          <DatePicker
            label="Date of birth"
            // TODO: make the datepicker small like the other fields
            // sx={{
            //   "& .MuiOutlinedInput-input": {
            //     py: 1.0625,
            //   },
            //   "& .MuiFormLabel-root": {
            //     transform: "translate(14px, 8.5px) scale(1)",
            //   },
            // }}
          />
        </MuiLocalizationProvider>
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
