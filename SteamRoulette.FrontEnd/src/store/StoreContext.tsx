import React from "react";
import authStore from "./AuthStore";

const StoreContext = React.createContext(authStore);

export const useStore = () => React.useContext(StoreContext);

export default StoreContext;
