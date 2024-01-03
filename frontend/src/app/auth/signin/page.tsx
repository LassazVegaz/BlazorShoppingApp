import AuthContainer from "@/components/AuthContainer";
import { Form } from "./components";
import serverAuth from "@/lib/server/server-auth";
import { redirect } from "next/navigation";

const SignInPage = async () => {
  if (await serverAuth.isAuthenticated()) redirect("/auth/profile");

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
