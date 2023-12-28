import { TextFieldProps, TextField } from "@mui/material";
import { FieldApi } from "@tanstack/react-form";

type MuiTanTextFieldProps = TextFieldProps & {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  field: FieldApi<any, any, any, any>;
};

const MuiTanTextField = ({ field, ...props }: MuiTanTextFieldProps) => (
  <TextField
    name={field.name}
    value={field.state.value ?? ""}
    onChange={(e) => field.handleChange(e.target.value)}
    onBlur={field.handleBlur}
    error={
      field.state.meta.isTouched && field.state.meta.touchedErrors.length > 0
    }
    helperText={field.state.meta.touchedErrors}
    {...props}
  />
);

export default MuiTanTextField;
