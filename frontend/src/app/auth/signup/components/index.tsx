"use client";
import { FormsFieldsContainer } from "@/components/AuthContainer/styled-components";
import MuiLocalizationProvider from "@/components/MuiLocalizationProvider";
import { Box, Button, Stack } from "@mui/material";
import useSignUpUtils from "../hooks";
import { validators } from "../helpers";
import { ValidationError } from "yup";
import { MuiTanDateField, MuiTanTextField } from "@/components/MuiTanFields";
import GenderDropdown from "./GenderDropdown";

export const Form = () => {
  const { form } = useSignUpUtils();

  return (
    <form.Provider>
      <Box
        py={5}
        maxHeight={"85vh"}
        sx={{
          overflow: "auto",
          "&::-webkit-scrollbar": {
            width: "0.4em",
          },
          "&::-webkit-scrollbar-track": {
            boxShadow: "inset 0 0 6px rgba(0,0,0,0.00)",
            webkitBoxShadow: "inset 0 0 6px rgba(0,0,0,0.00)",
          },
          "&::-webkit-scrollbar-thumb": {
            backgroundColor: "rgba(0,0,0,.1)",
            borderRadius: "4px",
          },
        }}
        component="form"
        onSubmit={(e) => {
          e.preventDefault();
          e.stopPropagation();
          form.handleSubmit();
        }}
      >
        <FormsFieldsContainer>
          <form.Field
            name="firstName"
            validators={{ onChange: validators.firstName }}
          >
            {(field) => (
              <MuiTanTextField field={field} label="First name" size="small" />
            )}
          </form.Field>
          <form.Field
            name="lastName"
            validators={{ onChange: validators.lastName }}
          >
            {(field) => (
              <MuiTanTextField field={field} label="Last name" size="small" />
            )}
          </form.Field>

          <form.Field name="email" validators={{ onChange: validators.email }}>
            {(field) => (
              <MuiTanTextField
                field={field}
                label="Email"
                type="email"
                size="small"
              />
            )}
          </form.Field>

          <form.Field
            name="dateOfBirth"
            validators={{ onChange: validators.dateOfBirth }}
          >
            {(field) => (
              <MuiLocalizationProvider>
                <MuiTanDateField
                  field={field}
                  label="Date of birth"
                  slotProps={{
                    textField: { size: "small" },
                  }}
                />
              </MuiLocalizationProvider>
            )}
          </form.Field>

          <form.Field
            name="gender"
            validators={{ onChange: validators.gender }}
          >
            {(field) => <GenderDropdown field={field} />}
          </form.Field>

          <form.Field
            name="password"
            validators={{ onChange: validators.password }}
          >
            {(field) => (
              <MuiTanTextField
                field={field}
                label="Password"
                type="password"
                size="small"
              />
            )}
          </form.Field>

          <form.Field
            name="passwordConfirmation"
            validators={{
              onChange: (data) => {
                try {
                  const password = data.fieldApi.form.getFieldValue("password");
                  validators
                    .passwordConfirmation(password)
                    .validateSync(data.value);
                } catch (error) {
                  if (error instanceof ValidationError) return error.message;
                  throw error;
                }
              },
            }}
          >
            {(field) => (
              <MuiTanTextField
                field={field}
                label="Confirm password"
                type="password"
                size="small"
              />
            )}
          </form.Field>
        </FormsFieldsContainer>

        <Stack alignItems="center" mt={5}>
          <form.Subscribe selector={(s) => s.isSubmitting}>
            {(isSubmitting) => (
              <Button variant="contained" type="submit" disabled={isSubmitting}>
                Sign Up
              </Button>
            )}
          </form.Subscribe>
        </Stack>
      </Box>
    </form.Provider>
  );
};
