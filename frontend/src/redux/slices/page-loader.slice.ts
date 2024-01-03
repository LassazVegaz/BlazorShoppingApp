import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

type PageLoaderState = {
  loading: boolean;
};

const initialState: PageLoaderState = {
  loading: false,
};

export const pageLoaderSlice = createSlice({
  name: "pageLoader",
  initialState,
  reducers: {
    setLoading: (state, action: PayloadAction<boolean>) => {
      state.loading = action.payload;
    },
  },
});

export const pageLoaderActions = pageLoaderSlice.actions;

export default pageLoaderSlice;
