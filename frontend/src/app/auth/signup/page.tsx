import AuthContainer from "@/components/AuthContainer";
import { Form } from "./components";
import serverAuth from "@/lib/server/server-auth";
import { redirect } from "next/navigation";

const SignUpPage = async () => {
  if (await serverAuth.isAuthenticated()) redirect("/auth/profile");

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
