import React from "react";
import ReactDOM from "react-dom/client";
import App from "./index";
import "../styles/globals.css";
import Routers from "./Routers";
const api = import.meta.env.VITE_API_URl;

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <Routers />
  </React.StrictMode>
);
