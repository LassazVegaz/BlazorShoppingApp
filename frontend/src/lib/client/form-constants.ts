export const genderValues = ["male", "female", "other"] as const;

const makeFirstLetterUppercase = (str: string) =>
  str[0].toUpperCase() + str.slice(1);

/**
 * Use to generate Gender field's dropdown options
 */
export const genderDropdowns = genderValues.map(
  (value) =>
    ({
      label: makeFirstLetterUppercase(value),
      value,
    } as const)
);
