import serverAuth from "@/lib/server/server-auth";
import { redirect } from "next/navigation";

export default async function Home() {
  if (await serverAuth.isAuthenticated()) redirect("/auth/profile");

  redirect("/auth/signin");
}
