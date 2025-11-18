import React, { useEffect, useState } from "react";
import AnimatedNumber from "./AnimatedNumber";
import authStore from "../src/store/AuthStore";

interface StatusBarProps {
  loggedIn: boolean;
  userName: string | null;
  points?: number;
  userimg: string | null;
}

var url = import.meta.env.VITE_API_URL;
var frontport = import.meta.env.VITE_CURRENT_PORT;

const redirect = () => {
  const returnUrl = `http://localhost:${frontport}/Authorization/token`;
  location.href = `${url}/api/auth/Login?returnUrl=${encodeURIComponent(returnUrl)}`;
};

const StatusBar: React.FC<StatusBarProps> = ({
  loggedIn,
  userName,
  points,
  userimg,
}) => {
  const [time, setTime] = useState<string>("");

  const notlogin = () => {
    return (
      <button
        className="w-full text-center h-full border-slate-700 border flex justify-left items-center bg-gradient-to-r from-gray-900 to-slate-800  rounded-lg relative"
        onClick={redirect}
      >
        <img width="33px" className="mx-4 " src="../public/SteamLogo.png"></img>

        <h3 className="flex text-xl">Login with steam</h3>
      </button>
    );
  };
  const login = () => {
    console.log(userimg);

    const logout = () => {
      authStore.clearToken();
      location.href = "/";
    };

    return (
      <>
        <div>
          <div className="border-slate-700 border flex justify-center items-center bg-gradient-to-r from-gray-900 to-slate-800 px-2 py-4 rounded-lg relative">
            <img
              src={userimg ? userimg : ""}
              className="w-[34px] h-[34px] rounded-lg relative"
            ></img>
            <span className="text-2xl font-bold">&nbsp;{userName}&nbsp;</span>
            <div className="sepparator w-px h-6 bg-gray-400 mx-4"></div>
            <button onClick={logout} className="font-bold text-sl">
              ВЫЙТИ
            </button>
          </div>
          <span className="absolute start-4 "></span>
        </div>
      </>
    );
  };

  useEffect(() => {
    const updateClock = () => {
      const now = new Date();
      const hours = now.getHours().toString().padStart(2, "0");
      const minutes = now.getMinutes().toString().padStart(2, "0");
      const seconds = now.getSeconds().toString().padStart(2, "0");
      setTime(`${hours}:${minutes}:${seconds}`);
    };

    const intervalId = setInterval(updateClock, 1000);
    updateClock(); // initial call to set the time immediately

    return () => clearInterval(intervalId); // cleanup interval on component unmount
  }, []);

  return (
    <div className="grid grid-cols-12 gap-4">
      <div className="col-span-4">
        <div className="border-slate-700 border flex justify-center items-center bg-gradient-to-r from-gray-900 to-slate-800 px-2 py-4 rounded-lg relative">
          <span className="absolute start-4">
            <svg
              className="w-[38px] h-[38px] text-gray-800 dark:text-white"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              fill="none"
              viewBox="0 0 24 24"
            >
              <path
                stroke="currentColor"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="m7.171 12.906-2.153 6.411 2.672-.89 1.568 2.34 1.825-5.183m5.73-2.678 2.154 6.411-2.673-.89-1.568 2.34-1.825-5.183M9.165 4.3c.58.068 1.153-.17 1.515-.628a1.681 1.681 0 0 1 2.64 0 1.68 1.68 0 0 0 1.515.628 1.681 1.681 0 0 1 1.866 1.866c-.068.58.17 1.154.628 1.516a1.681 1.681 0 0 1 0 2.639 1.682 1.682 0 0 0-.628 1.515 1.681 1.681 0 0 1-1.866 1.866 1.681 1.681 0 0 0-1.516.628 1.681 1.681 0 0 1-2.639 0 1.681 1.681 0 0 0-1.515-.628 1.681 1.681 0 0 1-1.867-1.866 1.681 1.681 0 0 0-.627-1.515 1.681 1.681 0 0 1 0-2.64c.458-.361.696-.935.627-1.515A1.681 1.681 0 0 1 9.165 4.3ZM14 9a2 2 0 1 1-4 0 2 2 0 0 1 4 0Z"
              />
            </svg>
          </span>
          <span className="text-2xl font-bold">
            {loggedIn && points !== undefined ? (
              <AnimatedNumber value={points} speedMs={1000} className="flex" />
            ) : (
              <span>&nbsp;</span>
            )}
          </span>
        </div>
      </div>
      <div className="col-span-4">
        <div className="border-slate-700 border flex justify-center items-center bg-gradient-to-r from-gray-900 to-slate-800 px-2 py-4 rounded-lg relative">
          <span className="absolute start-4">
            <svg
              className="w-[38px] h-[38px] text-gray-800 dark:text-white"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              fill="none"
              viewBox="0 0 24 24"
            >
              <path
                stroke="currentColor"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M12 8v4l3 3m6-3a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
              />
            </svg>
          </span>
          <span className="text-2xl font-bold">
            &nbsp;{loggedIn && time ? time : ""}&nbsp;
          </span>
        </div>
      </div>
      <div className="col-span-4">{loggedIn ? login() : notlogin()}</div>
    </div>
  );
};

export default StatusBar;
