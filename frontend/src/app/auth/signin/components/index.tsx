"use client";
import { FormsFieldsContainer } from "@/components/AuthContainer/styled-components";
import { Box, Stack, Button } from "@mui/material";
import useSignInUtils from "../hooks";
import { validators } from "../helpers";
import { MuiTanTextField } from "@/components/MuiTanFields";

export const Form = () => {
  const { form } = useSignInUtils();

  return (
    <form.Provider>
      <Box
        component="form"
        onSubmit={(e) => {
          e.preventDefault();
          e.stopPropagation();
          form.handleSubmit();
        }}
      >
        <FormsFieldsContainer>
          <form.Field
            name="email"
            validators={{
              onChange: validators.email,
            }}
          >
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
            name="password"
            validators={{
              onChange: validators.password,
            }}
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
        </FormsFieldsContainer>

        <Stack alignItems="center" mt={5}>
          <form.Subscribe selector={(s) => s.isSubmitted}>
            {(isSubmitted) => (
              <Button variant="contained" type="submit" disabled={isSubmitted}>
                Sign In
              </Button>
            )}
          </form.Subscribe>
        </Stack>
      </Box>
    </form.Provider>
  );
};
