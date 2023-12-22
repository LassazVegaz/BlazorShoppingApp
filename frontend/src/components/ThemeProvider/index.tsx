"use client";
import theme from "@/lib/theme";
import { ThemeProvider as MuiThemeProvider } from "@mui/material";

const ThemeProvider = ({ children }: { children: React.ReactNode }) => (
  <MuiThemeProvider theme={theme}>{children}</MuiThemeProvider>
);

export default ThemeProvider;
