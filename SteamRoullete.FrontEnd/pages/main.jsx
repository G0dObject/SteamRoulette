import React from "react";
import ReactDOM from "react-dom/client";
import App from "./index";
import "../styles/globals.css";

const api = import.meta.env.VITE_API_URl;





ReactDOM.createRoot(document.getElementById("root")).render(
  <>
   

    <App />
  </>
);

