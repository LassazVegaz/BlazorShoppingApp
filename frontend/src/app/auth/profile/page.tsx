import {
  Button,
  Container,
  Paper,
  Stack,
  TextField,
  Typography,
} from "@mui/material/index";

const SignUpPage = () => {
  return (
    <Container>
      <Typography variant="h2">Profile</Typography>

      <Paper>
        <Typography variant="h4">Basic information</Typography>

        <Stack>
          <TextField label="First name" />
          <TextField label="Last name" />
          <TextField label="Date of birth" type="date" />
          <TextField label="Gender" />
        </Stack>

        <Button color="secondary">Reset</Button>
        <Button>Save</Button>
      </Paper>

      <Paper>
        <Typography variant="h4">Email</Typography>

        <TextField label="Email" type="email" />

        <Typography variant="body1">
          You can change your email address only once every 30 days.
        </Typography>

        <Button color="secondary">Reset</Button>
        <Button>Save</Button>
      </Paper>

      <Paper>
        <Typography variant="h4">Password</Typography>

        <Stack>
          <TextField label="Current password" type="password" />
          <TextField label="Password" type="password" />
          <TextField label="Confirm password" type="password" />
        </Stack>

        <Button>Save</Button>
      </Paper>
    </Container>
  );
};

export default SignUpPage;
