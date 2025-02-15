import React, { useEffect, useState } from "react";
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
  location.href = `${url}/api/auth/Login?ReturnUrl=http://localhost:${frontport}/Authorization/token?`;
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
        <img width="33px" className="mx-4 " src="/SteamLogo.png"></img>

        <h3 className="flex text-xl">Login with steam</h3>
      </button>
    );
  };
  const login = () => {
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
              width="38px"
              height="38px"
              viewBox="0 0 1024 1024"
              className="icon"
              version="1.1"
              xmlns="http://www.w3.org/2000/svg"
              fill="#ffffff"
            >
              <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
              <g
                id="SVGRepo_tracerCarrier"
                strokeLinecap="round"
                strokeLinejoin="round"
              ></g>
              <g id="SVGRepo_iconCarrier">
                <path
                  d="M951.87 253.86c0-82.18-110.05-144.14-256-144.14s-256 61.96-256 144.14c0 0.73 0.16 1.42 0.18 2.14h-0.18v109.71h73.14v-9.06c45.77 25.81 109.81 41.33 182.86 41.33 67.39 0 126.93-13.33 171.71-35.64 6.94 7.18 11.15 14.32 11.15 20.58 0 28.25-72.93 70.98-182.86 70.98h-73.12v73.14h73.12c67.4 0 126.96-13.33 171.74-35.65 6.95 7.17 11.11 14.31 11.11 20.6 0 28.27-72.93 71-182.86 71l-25.89 0.12c-15.91 0.14-31.32 0.29-46.34-0.11l-1.79 73.11c8.04 0.2 16.18 0.27 24.48 0.27 7.93 0 16-0.05 24.2-0.12l25.34-0.12c67.44 0 127.02-13.35 171.81-35.69 6.97 7.23 11.04 14.41 11.04 20.62 0 28.27-72.93 71-182.86 71h-73.12v73.14h73.12c67.44 0 127.01-13.35 171.81-35.69 6.98 7.22 11.05 14.4 11.05 20.62 0 28.27-72.93 71-182.86 71h-73.12v73.14h73.12c145.95 0 256-61.96 256-144.14 0-0.68-0.09-1.45-0.11-2.14h0.11V256h-0.18c0.03-0.72 0.2-1.42 0.2-2.14z m-438.86 0c0-28.27 72.93-71 182.86-71s182.86 42.73 182.86 71c0 28.25-72.93 70.98-182.86 70.98s-182.86-42.73-182.86-70.98z"
                  fill="#ffffff"
                ></path>
                <path
                  d="M330.15 365.71c-145.95 0-256 61.96-256 144.14 0 0.73 0.16 1.42 0.18 2.14h-0.18v256c0 82.18 110.05 144.14 256 144.14s256-61.96 256-144.14V512h-0.18c0.02-0.72 0.18-1.42 0.18-2.14 0-82.18-110.05-144.15-256-144.15zM147.29 638.93c0-6.32 4.13-13.45 11.08-20.62 44.79 22.33 104.36 35.67 171.78 35.67 67.39 0 126.93-13.33 171.71-35.64 6.94 7.18 11.15 14.32 11.15 20.58 0 28.25-72.93 70.98-182.86 70.98s-182.86-42.72-182.86-70.97z m182.86-200.07c109.93 0 182.86 42.73 182.86 71 0 28.25-72.93 70.98-182.86 70.98s-182.86-42.73-182.86-70.98c0-28.27 72.93-71 182.86-71z m0 400.14c-109.93 0-182.86-42.73-182.86-71 0-6.29 4.17-13.43 11.11-20.6 44.79 22.32 104.34 35.66 171.75 35.66 67.4 0 126.96-13.33 171.74-35.65 6.95 7.17 11.11 14.31 11.11 20.6 0.01 28.26-72.92 70.99-182.85 70.99z"
                  fill="#ffffff"
                ></path>
              </g>
            </svg>
          </span>
          <span className="text-2xl font-bold">
            {loggedIn && points !== undefined ? <>500</> : <span>&nbsp;</span>}
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
