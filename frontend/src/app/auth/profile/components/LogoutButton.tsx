"use client";
import authApi from "@/lib/client/auth-api";
import clientTokens from "@/lib/client/client-tokens";
import { pageLoaderActions } from "@/redux/slices/page-loader.slice";
import { Logout as LogoutIcon } from "@mui/icons-material";
import { Fab } from "@mui/material";
import { useRouter } from "next/navigation";
import { useCallback } from "react";
import { useDispatch } from "react-redux";

const _logout = async () => {
  clientTokens.removeToken();
  await authApi.logout();
};

const LogoutButton = () => {
  const dispatch = useDispatch();
  const router = useRouter();

  const onLogout = useCallback(async () => {
    dispatch(pageLoaderActions.setLoading(true));
    try {
      await _logout();
    } catch (error) {
    } finally {
      dispatch(pageLoaderActions.setLoading(false));
      router.push("/");
    }
  }, [dispatch, router]);

  return (
    <Fab
      size="small"
      color="secondary"
      sx={{
        top: 20,
        right: 20,
        position: "fixed",
      }}
      onClick={onLogout}
    >
      <LogoutIcon fontSize="small" />
    </Fab>
  );
};

export default LogoutButton;
