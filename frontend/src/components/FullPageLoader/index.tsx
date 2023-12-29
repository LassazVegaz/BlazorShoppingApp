"use client";
import { Backdrop, CircularProgress } from "@mui/material";

/**
 * Screen-wide global loader
 */
const FullPageLoader = () => {
  return (
    <Backdrop open={false}>
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
