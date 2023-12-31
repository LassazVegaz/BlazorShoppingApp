// I had to duplicate because, LOOK AT IT. making an abstraction out of this will make
// it more complicated than it already is. I don't want to do that. I want to keep it simple.
import {
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  FormHelperText,
} from "@mui/material";
import { FieldApi } from "@tanstack/react-form";
import { genderDropdowns } from "@/lib/client/form-constants";

type GenderDropdownProps = {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  field: FieldApi<any, any, any, any>;
};

const GenderDropdown = ({ field }: GenderDropdownProps) => {
  const hasError = field.state.meta.touchedErrors.length > 0;
  return (
    <FormControl>
      <InputLabel id="signup-gender-field" error={hasError}>
        Gender
      </InputLabel>
      <Select
        name={field.name}
        labelId="signup-gender-field"
        value={field.state.value ?? ""}
        label="Gender"
        onBlur={field.handleBlur}
        onChange={(v) => field.handleChange(v.target.value)}
        error={hasError}
      >
        {genderDropdowns.map((o) => (
          <MenuItem key={o.value} value={o.value}>
            {o.label}
          </MenuItem>
        ))}
      </Select>
      {hasError && (
        <FormHelperText error>{field.state.meta.touchedErrors}</FormHelperText>
      )}
    </FormControl>
  );
};

export default GenderDropdown;
