import AuthContainer from "@/components/AuthContainer";
import { Form } from "./components";

const SignUpPage = () => {
  return (
    <AuthContainer
      leftPanel={{
        title: "Welcome!",
        subtitle: "Already have an account?",
        buttonText: "Sign In",
        buttonLink: "/auth/signin",
      }}
    >
      <Form />
    </AuthContainer>
  );
};

export default SignUpPage;
