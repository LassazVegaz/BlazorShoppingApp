"use client";
import { Paper, PaperProps, Stack, Typography } from "@mui/material";

type FormSectionProps = {
  title: string;
  children?: React.ReactNode;
  buttons?: React.ReactNode;
  onSubmit?: PaperProps<"form">["onSubmit"];
};

const FormSection = (props: FormSectionProps) => (
  <Paper
    component="form"
    onSubmit={(e) => {
      e.preventDefault();
      e.stopPropagation();
      props.onSubmit?.(e);
    }}
    elevation={3}
    sx={{
      p: 2,
    }}
  >
    <Typography variant="h2" fontSize={32} mb={2} textAlign="center">
      {props.title}
    </Typography>

    <Stack gap={1} mb={4}>
      {props.children}
    </Stack>

    <Stack direction="row" justifyContent="center" gap={5}>
      {props.buttons}
    </Stack>
  </Paper>
);

export default FormSection;
