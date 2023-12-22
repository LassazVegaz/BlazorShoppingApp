import { Container, Grid, Typography, Button } from "@mui/material";
import Link from "next/link";

type AuthContainerProps = {
  /**Content of the right side panel */
  children?: React.ReactNode;
  /**Content of left panel */
  leftPanel: {
    title: string;
    subtitle: string;
    buttonText: string;
    buttonLink: string;
  };
};

/**
 * Common container (background and stuff) used in both signin and signup pages
 */
const AuthContainer = (props: AuthContainerProps) => {
  return (
    <Container sx={{ height: "100vh", display: "flex", alignItems: "center" }}>
      <Grid
        container
        minHeight="85vh"
        boxShadow="0px 0px 10px 0px rgba(0,0,0,0.75)"
        borderRadius={2}
        overflow="hidden"
      >
        {/* left panel */}
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
            {props.leftPanel.title}
          </Typography>

          <Typography variant="body1" fontSize={25} mt={2} textAlign="center">
            {props.leftPanel.subtitle}
          </Typography>

          <Link href={props.leftPanel.buttonLink}>
            <Button
              variant="outlined"
              sx={{
                mt: 5,
                color: "white",
                borderColor: "white",
                "&:hover": {
                  borderColor: "wheat",
                },
              }}
            >
              {props.leftPanel.buttonText}
            </Button>
          </Link>
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
          {props.children}
        </Grid>
      </Grid>
    </Container>
  );
};

export default AuthContainer;
