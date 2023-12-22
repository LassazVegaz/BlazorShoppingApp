import {
  Button,
  Container,
  Stack,
  TextField,
  Typography,
} from "@mui/material/index";

const SignUpPage = () => {
  return (
    <Container>
      <Typography variant="h2">Sign Up</Typography>

      <Stack>
        <TextField label="First name" />
        <TextField label="Last name" />
        <TextField label="Email" type="email" />
        <TextField label="Date of birth" type="date" />
        <TextField label="Gender" />
        <TextField label="Password" type="password" />
        <TextField label="Confirm password" type="password" />
      </Stack>

      <Button>Sign Up</Button>
    </Container>
  );
};

export default SignUpPage;
