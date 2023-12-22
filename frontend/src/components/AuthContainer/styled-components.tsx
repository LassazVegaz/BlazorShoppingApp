"use client";
import { Stack, styled } from "@mui/material";

export const FormsFieldsContainer = styled(Stack)(({ theme }) => ({
  paddingLeft: theme.spacing(20),
  paddingRight: theme.spacing(20),
  gap: theme.spacing(2),
}));
