// src/components/AuthCallback.tsx
import React, { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useStore } from "../src/store/StoreContext";

const AuthCallback: React.FC = () => {
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token");
  const authStore = useStore();
  const navigate = useNavigate();

  useEffect(() => {
    if (token) {
      authStore.setToken(token);
      console.log(authStore.token);

      navigate("/"); // Перенаправляем пользователя на главную страницу после сохранения токена
    }
  }, [token, authStore, navigate]);

  return (
    <div>
      <p>Processing authentication...</p>
    </div>
  );
};

export default AuthCallback;
