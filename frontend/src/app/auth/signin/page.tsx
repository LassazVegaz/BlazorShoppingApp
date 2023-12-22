import {
  Button,
  Container,
  Grid,
  Stack,
  TextField,
  Typography,
} from "@mui/material";

const SignInPage = () => {
  return (
    <Container sx={{ height: "100vh", display: "flex", alignItems: "center" }}>
      <Grid
        container
        minHeight="85vh"
        boxShadow="0px 0px 10px 0px rgba(0,0,0,0.75)"
        borderRadius={2}
        overflow="hidden"
      >
        <Grid
          item
          xs={4}
          display="flex"
          flexDirection="column"
          justifyContent="center"
          alignItems="center"
          bgcolor="primary.main"
        >
          <Typography
            variant="h1"
            fontSize={40}
            textAlign="center"
            fontWeight="bold"
          >
            Welcome Back!
          </Typography>

          <Typography variant="body1" fontSize={25} mt={2} textAlign="center">
            Still don&apos;t have an account?
          </Typography>

          <Button
            variant="outlined"
            sx={{
              mt: 5,
              color: "white",
              borderColor: "white",
            }}
          >
            Sign Up
          </Button>
        </Grid>

        {/* form */}
        <Grid
          item
          xs={8}
          pl={5}
          display="flex"
          flexDirection="column"
          justifyContent="center"
        >
          <Stack px={20} gap={2}>
            <TextField label="Email" type="email" size="small" />
            <TextField label="Password" type="password" size="small" />
          </Stack>

          <Stack alignItems="center" mt={5}>
            <Button variant="contained">Sign In</Button>
          </Stack>
        </Grid>
      </Grid>
    </Container>
  );
};

export default SignInPage;
