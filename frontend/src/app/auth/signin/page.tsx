import {
  Button,
  Container,
  Stack,
  TextField,
  Typography,
} from "@mui/material/index";

const SignInPage = () => {
  return (
    <Container>
      <Typography variant="h2">Sign In</Typography>

      <Stack>
        <TextField label="Email" type="email" />
        <TextField label="Password" type="password" />
      </Stack>

      <Button>Sign In</Button>
    </Container>
  );
};

export default SignInPage;
