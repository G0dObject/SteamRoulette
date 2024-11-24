import { defineConfig, loadEnv } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig(({ mode }) => {
  // Загрузка переменных окружения
  const env = loadEnv(mode, process.cwd(), '');

  return {
    plugins: [react()],
    server: {
      port: env.VITE_PORT ? parseInt(env.VITE_PORT, 10) : 3000, // Используем порт из переменной окружения или 3000 по умолчанию
    },
  };
});