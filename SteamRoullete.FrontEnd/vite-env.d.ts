interface ImportMetaEnv {
    readonly VITE_API_URL: string;
    readonly VITE_CURRENT_PORT: string;
    // Add other environment variables here
  }
  
  interface ImportMeta {
    readonly env: ImportMetaEnv;
  }