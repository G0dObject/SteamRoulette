// src/App.tsx
import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { Provider } from "react-redux";
import store from "../src/store/AuthStore";
import AuthCallback from "../components/AuthCallback";
import App from "./index";
import authStore from "../src/store/AuthStore";
import StoreContext from "../src/store/StoreContext";

const Routers: React.FC = () => {
  return (
    <StoreContext.Provider value={authStore}>
      <Router>
        <Routes>
          <Route path="/" element={<App />} />
          <Route path="/Authorization/token" element={<AuthCallback />} />
        </Routes>
      </Router>
    </StoreContext.Provider>
  );
};

export default Routers;
