"use client";
import { Button, styled } from "@mui/material";

export const FormButton = styled(Button)(({ theme }) => ({
  width: "100%",
  textTransform: "none",
  [theme.breakpoints.up("sm")]: {
    width: 100,
  },
}));
