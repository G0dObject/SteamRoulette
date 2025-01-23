import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import svgr from 'vite-plugin-svgr';

// https://vitejs.dev/config/
export default defineConfig({
  define: {
    'import.meta.env.VITE_API_URL': JSON.stringify(process.env.services__WebApi__https__0),
  },
 server:{port: process.env.VITE_PORT},
  // server:{port: 5000},

  plugins: [react(), svgr()]
})
