import { useState } from "react";

interface WelcomeLoginProps {
  onLoginSuccess: (name: string) => void; // Callback for successful login
}

const WelcomeLogin: React.FC<WelcomeLoginProps> = ({ onLoginSuccess }) => {
  const [name, setName] = useState<string>("");
  const [error, setError] = useState<string>("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setName(e.target.value);
    setError(""); // Clear any previous error message
  };



  return (
    <div className="flex flex-col justify-stretch items-center h-full bg-gray-900">
      <div className="w-full bg-gray-800 p-10 pb-20 rounded-lg shadow-lg text-center flex flex-col h-full justify-center">
        <h2 className="text-white text-2xl mb-24">Welcome</h2>
        
      </div>
    </div>
  );
};

export default WelcomeLogin;
