"use client";
import { useAppSelector } from "@/redux/hooks";
import { Backdrop, CircularProgress } from "@mui/material";

/**
 * Screen-wide global loader
 */
const FullPageLoader = () => {
  const open = useAppSelector((s) => s.pageLoader.loading);

  return (
    <Backdrop open={open}>
      <CircularProgress
        size={60}
        thickness={5}
        sx={{
          color: "primary.dark",
        }}
      />
    </Backdrop>
  );
};

export default FullPageLoader;
