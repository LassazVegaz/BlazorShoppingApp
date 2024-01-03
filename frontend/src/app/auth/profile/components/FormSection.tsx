"use client";
import {
  Box,
  CircularProgress,
  Paper,
  PaperProps,
  Stack,
  Typography,
} from "@mui/material";

type FormSectionProps = Pick<PaperProps<"form">, "onSubmit" | "onReset"> & {
  title: string;
  children?: React.ReactNode;
  buttons?: React.ReactNode;
  isLoading?: boolean;
};

const FormSection = (props: FormSectionProps) => (
  <Paper
    component="form"
    onReset={props.onReset}
    onSubmit={props.onSubmit}
    elevation={3}
    sx={{
      p: 2,
      position: "relative",
      overflow: "hidden",
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

    <Box
      position="absolute"
      top={0}
      left={0}
      width="100%"
      height="100%"
      display={props.isLoading ? "flex" : "none"}
      justifyContent="center"
      alignItems="center"
      sx={{
        background: "radial-gradient(transparent, #bfbfbf2e)",
      }}
    >
      <CircularProgress size={64} />
    </Box>
  </Paper>
);

export default FormSection;
