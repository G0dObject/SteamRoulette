import React, { useState, useEffect, useRef } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

import WelcomeLogin from "../components/WelcomeLogin";
import StatusBar from "../components/StatusBar";
import NumberInput from "../components/NumberInput";
import MultiplierGraph from "../components/Rocket";
import UserInventory from "../components/UserInventory";
import Ranking from "../components/Ranking";
import Chat from "../components/Chat";
import AnimatedNumber from "../components/AnimatedNumber";
import YouLose from "../components/YouLose";

import styles from "../styles/Root.module.scss";
import authStore from "../src/store/AuthStore";
import { getInventoryByToken, SteamItem } from "../src/Api/inventory";
import { GraphWrapper } from "../components/GraphWrapper";


const Home: React.FC = () => {
  const [isGameRunning, setIsGameRunning] = useState<boolean>(false); // Флаг запуска игры
  const [result, setResult] = useState<number>(1); // Текущее значение
  const [isCrush, setIsCrush] = useState<boolean>(false); // Флаг краха
  const [crushValue, setCrushValue] = useState<number | null>(null); // Значение краха
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [userName, setUserName] = useState<string | null>("");
  const [userImg, setUserImg] = useState<string | null>("");
  const [userPoints, setUserPoints] = useState<number>(1000); // Example points
  const [userWon, setUserWon] = useState<boolean>();
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [countdown, setCountdown] = useState<number | null>(null); // Обратный отсчет
  const countdownIntervalRef = useRef<NodeJS.Timeout | null>(null);
  const [userItems, setUserItems] = useState<SteamItem[]>([]);

  useEffect(() => {
    checkauth();
    getItems();
    // Создаем подключение к SignalR Hub
    const newConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:7069/gameHub") // URL вашего GameHub
      .withAutomaticReconnect() // Автоматически переподключаться при потере соединения
      .build();

    setConnection(newConnection);

    // Начинаем подключение
    newConnection
      .start()
      .then(() => console.log("Connected to the game hub!"))
      .catch((err) => console.error("Failed to connect to the game hub:", err));

    // Подписываемся на событие PreparationGame
    newConnection.on("PreparationGame", () => {
      console.log("ready???");
      setCountdown(10); // Начинаем обратный отсчет с 10
      countdownIntervalRef.current = setInterval(() => {
        setCountdown((prevCountdown) => {
          if (prevCountdown === null || prevCountdown <= 0) {
            clearInterval(countdownIntervalRef.current!);
            return null;
          }
          return prevCountdown - 0.2; // Уменьшаем на 0.2 каждую секунду
        });
      }, 200); // Обновляем каждые 200 мс
    });

    // Подписываемся на событие Start
    newConnection.on("Start", () => {
      console.log("Go!!!");
      clearInterval(countdownIntervalRef.current!);
      setCountdown(null); // Останавливаем обратный отсчет
      setIsGameRunning(true); // Устанавливаем флаг запуска игры
      setIsCrush(false); // Сбрасываем флаг краха
      setCrushValue(null); // Сбрасываем значение краха
      setResult(1); // Сбрасываем текущее значение на 1
    });

    // Подписываемся на событие Crush
    newConnection.on("Crush", (crashValue) => {
      console.log(`Game crushed at: ${crashValue}`);
      // Устанавливаем флаг краха и значение краха
      setIsCrush(true);
      setCrushValue(crashValue);

      // Останавливаем игру
      setIsGameRunning(false);
    });

    // Очищаем подключение при размонтировании компонента
    return () => {
      newConnection.stop();
      clearInterval(countdownIntervalRef.current!);
    };
  }, []);


  const getItems = ()=>{
   getInventoryByToken().then((res)=>{
    setUserItems(res)
   })
  }

  const checkauth = () => {
    if (localStorage.getItem("authToken")) {
      setIsLoggedIn(true);
      setUserName(authStore.name);
      setUserImg(authStore.img);
    }
  };

  return (
    <div className="py-10 px-5">
      <div className="container mx-auto px-8">
        <div className="grid grid-cols-12 gap-6">
          {!isLoggedIn ? (
            <div className="col-span-4">Auth with steam for game</div>
          ) : (
            <div className="col-span-4">
              <div className="styles.gameHeader">
                <div className="flex justify-center items-center columns-6 gap-3"></div>
              </div>
              <button
                
                className="text-white font-bold py-4  mb-4 px-4 rounded bg-gradient-to-r from-pink-500 to-red-500 w-full "
              >
                <div className="text-2xl font-bold">Начать</div>
              </button>

              <UserInventory items={userItems} />
            </div>
          )}
         <GraphWrapper userWon={userWon as boolean} isLoggedIn={isLoggedIn} userName={userName as string} userImg={userImg as string} userPoints={userPoints} result={result}   />
        </div>
      </div>
      <div className="container mx-auto px-8">
        <div className="grid grid-cols-12 gap-4">
          <div className="col-span-6"></div>
          <div className="col-span-6"></div>
        </div>
      </div>
    </div>
  );
};

export default Home;