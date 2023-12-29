import { TextFieldProps, TextField } from "@mui/material";
import { DatePicker, DatePickerProps } from "@mui/x-date-pickers";
import { FieldApi } from "@tanstack/react-form";
import { Dayjs } from "dayjs";

type MuiTanTextFieldProps = TextFieldProps & {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  field: FieldApi<any, any, any, any>;
};

export const MuiTanTextField = ({ field, ...props }: MuiTanTextFieldProps) => (
  <TextField
    name={field.name}
    value={field.state.value ?? ""}
    onChange={(e) => field.handleChange(e.target.value)}
    onBlur={field.handleBlur}
    error={field.state.meta.touchedErrors.length > 0}
    helperText={field.state.meta.touchedErrors}
    {...props}
  />
);

type MuiTanDateFieldProps = DatePickerProps<Dayjs> & {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  field: FieldApi<any, any, any, any>;
};

export const MuiTanDateField = ({
  field,
  slotProps,
  ...props
}: MuiTanDateFieldProps) => (
  <DatePicker
    name={field.name}
    value={field.state.value ?? null}
    onChange={(v) => field.handleChange(v)}
    {...props}
    slotProps={{
      ...slotProps,
      textField: {
        error: field.state.meta.touchedErrors.length > 0,
        helperText: field.state.meta.touchedErrors,
        ...slotProps?.textField,
      },
    }}
  />
);
