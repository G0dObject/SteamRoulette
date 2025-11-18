// src/components/AuthCallback.tsx
import React, { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useStore } from "../src/store/StoreContext";

const AuthCallback: React.FC = () => {
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token");
  const authStore = useStore();
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (token) {
      try {
        authStore.setToken(token);
        // Небольшая задержка для сохранения токена
        setTimeout(() => {
          navigate("/");
        }, 100);
      } catch (err) {
        setError("Ошибка при обработке токена");
        console.error("Token processing error:", err);
      }
    } else {
      // Если токена нет через 3 секунды, показываем ошибку
      const timeout = setTimeout(() => {
        setError("Токен не получен. Попробуйте войти снова.");
      }, 3000);

      return () => clearTimeout(timeout);
    }
  }, [token, authStore, navigate]);

  if (error) {
    return (
      <div style={{ padding: "20px", textAlign: "center" }}>
        <p style={{ color: "red" }}>{error}</p>
        <button onClick={() => navigate("/")}>Вернуться на главную</button>
      </div>
    );
  }

  return (
    <div style={{ padding: "20px", textAlign: "center" }}>
      <p>Processing authentication...</p>
    </div>
  );
};

export default AuthCallback;
