import React, { useState } from "react";

const LoginComponent = () => {
  const [responseData, setResponseData] = useState(null);
  const [error, setError] = useState(null);

  var url = import.meta.env.VITE_API_URL;
  var frontport = import.meta.env.VITE_CURRENT_PORT;
console.log(url, frontport);

  const redirect = () => {
    location.href = `${url}/api/auth/Login?ReturnUrl=http://localhost:${frontport}`;
  };

  return (
    <div>
      <button onClick={redirect}>Login</button>
      {responseData && (
        <div>
          <h2>Response Data:</h2>
          <pre>{JSON.stringify(responseData, null, 2)}</pre>
        </div>
      )}
      {error && (
        <div>
          <h2>Error:</h2>
          <p>{error}</p>
        </div>
      )}
    </div>
  );
};

export default LoginComponent;
