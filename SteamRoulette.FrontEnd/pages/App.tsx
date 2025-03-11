import React, { useState, useEffect, useRef } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import styles from "../styles/Root.module.scss";
import authStore from "../src/store/AuthStore";
import { getInventoryByToken, SteamItem } from "../src/Api/inventory";
import Content from "./Content";

const Home: React.FC = () => {
  const [isGameRunning, setIsGameRunning] = useState<boolean>(true);
  const [result, setResult] = useState<number>(1);
  const [isCrush, setIsCrush] = useState<boolean>(false);
  const [crushValue, setCrushValue] = useState<number | null>(null);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [userName, setUserName] = useState<string | null>("");
  const [userImg, setUserImg] = useState<string | null>("");
  const [userPoints, setUserPoints] = useState<number>(1000);
  const [userWon, setUserWon] = useState<boolean>();
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );
  const [countdown, setCountdown] = useState<number | null>(null);
  const countdownIntervalRef = useRef<NodeJS.Timeout | null>(null);
  const [userItems, setUserItems] = useState<SteamItem[]>([]);

  useEffect(() => {
    checkAuth();

    const newConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:7069/gameHub")
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection
      .start()
      .then(() => console.log("Connected to the game hub!"))
      .catch((err) => console.error("Failed to connect to the game hub:", err));

    newConnection.on("PreparationGame", handlePreparationGame);
    newConnection.on("Start", handleGameStart);
    newConnection.on("Crush", handleGameCrush);

    return () => {
      newConnection.stop();
      clearInterval(countdownIntervalRef.current!);
    };
  }, []);



  const checkAuth = () => {
    if (localStorage.getItem("authToken")) {
      setIsLoggedIn(true);
      setUserName(authStore.name);
      setUserImg(authStore.img);
    }
  };

  const handlePreparationGame = () => {
    console.log("ready???");
    setCountdown(10);
    countdownIntervalRef.current = setInterval(() => {
      setCountdown((prevCountdown) => {
        if (prevCountdown === null || prevCountdown <= 0) {
          clearInterval(countdownIntervalRef.current!);
          return null;
        }
        return prevCountdown - 0.2;
      });
    }, 200);
  };

  const handleGameStart = () => {
    console.log("Go!!!");
    clearInterval(countdownIntervalRef.current!);
    setCountdown(null);
    setIsGameRunning(true);
    setIsCrush(false);
    setCrushValue(null);
    setResult(1);
  };

  const handleGameCrush = (crashValue: number) => {
    console.log(`Game crushed at: ${crashValue}`);
    setIsCrush(true);
    setCrushValue(crashValue);
    setIsGameRunning(false);
  };

  return (
    <><Content/></>
  );
};

export default Home;