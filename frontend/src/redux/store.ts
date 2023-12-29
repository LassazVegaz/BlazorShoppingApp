import { configureStore } from "@reduxjs/toolkit";
import pageLoaderSlice from "./slices/page-loader.slice";

export const store = configureStore({
  reducer: {
    pageLoader: pageLoaderSlice.reducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
