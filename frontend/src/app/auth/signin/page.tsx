import AuthContainer from "@/components/AuthContainer";
import { Form } from "./components";

const SignInPage = () => {
  return (
    <AuthContainer
      leftPanel={{
        title: "Welcome Back!",
        subtitle: "Still don't have an account?",
        buttonText: "Sign Up",
        buttonLink: "/auth/signup",
      }}
    >
      <Form />
    </AuthContainer>
  );
};

export default SignInPage;
