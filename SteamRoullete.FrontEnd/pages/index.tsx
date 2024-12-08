import React, { useState, useEffect, useCallback } from "react";

import WelcomeLogin from "../components/WelcomeLogin";
import StatusBar from "../components/StatusBar";
import NumberInput from "../components/NumberInput";
import MultiplierGraph from "../components/MultiplierGraph";

import UserInventory from "../components/UserInventory";
import Ranking from "../components/Ranking";
import Chat from "../components/Chat";
import AnimatedNumber from "../components/AnimatedNumber";
import YouLose from "../components/YouLose";

import styles from "../styles/Root.module.scss";
import authStore from "../src/store/AuthStore";

interface Player {
  name: string;
  points: number;
  multiplier: number;
}

interface GraphDataPoints {
  name: string;
  multiplier?: number;
}

const defaultData: GraphDataPoints[] = [
  { name: "0" },
  { name: "1" },
  { name: "2" },
  { name: "3" },
  { name: "4" },
  { name: "5" },
  { name: "6" },
  { name: "7" },
  { name: "8" },
  { name: "9" },
  { name: "10" },
];

const generateGraphData = (): GraphDataPoints[] => {
  const data: GraphDataPoints[] = [];
  const lastResult = parseFloat((Math.random() * 11).toFixed(2)); // Random value for the last round
  data.push({ name: "10", multiplier: lastResult });

  // Generate the initial rounds based on the random value of the last round
  let currentResult = lastResult;
  for (let i = 9; i >= 0; i--) {
    currentResult *= 0.85; // Decrement factor for a downward curve
    data.push({
      name: `${i}`,
      multiplier: parseFloat(currentResult.toFixed(2)),
    });
  }

  return data.reverse(); // Reverse the data array to start with the first round
};

const getRandomValueFromMultiples = (
  min: number,
  max: number,
  step: number
) => {
  const multiples = Array.from(
    { length: Math.floor((max - min) / step) + 1 },
    (_, i) => min + i * step
  );
  return multiples[Math.floor(Math.random() * multiples.length)];
};

const randomPoints = () => getRandomValueFromMultiples(50, 1000, 25);
const randomMultipliers = () =>
  parseFloat(getRandomValueFromMultiples(1.0, 10, 0.25).toFixed(2));

const Home: React.FC = () => {
  const [multiplier, setMultiplier] = useState<number>(2.0); // Min and default value is 1.00, increment by 0.25
  const [speed, setSpeed] = useState<number>(1);
  const [speedMs, setSpeedMs] = useState<number>(5000);
  const [data, setData] = useState<GraphDataPoints[]>(defaultData);
  const [result, setResult] = useState<number>(0);
  const [chartKey, setChartKey] = useState<number>(0);
  const [round, setRound] = useState<Player[]>([]);
  const [ranking, setRanking] = useState<Player[]>([]);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [userName, setUserName] = useState<string | null>("");
  const [userImg, setUserImg] = useState<string | null>("");
  const [userPoints, setUserPoints] = useState<number>(1000); // Example points
  const [userWon, setUserWon] = useState<boolean>();

  useEffect(() => {
    checkauth();
  }, []);

  const checkauth = () => {
    if (localStorage.getItem("authToken")) {
      setIsLoggedIn(true);
      //WARNING
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
                onClick={() => {}}
                className="text-white font-bold py-4  mb-4 px-4 rounded bg-gradient-to-r from-pink-500 to-red-500 w-full "
              >
                <div className="text-2xl font-bold">Start</div>
              </button>

              <UserInventory />
            </div>
          )}
          <div className="col-span-8 relative">
            {userWon != null ? <YouLose userWon={userWon} /> : null}
            <StatusBar
              loggedIn={isLoggedIn}
              userName={userName}
              points={userPoints}
              userimg={userImg}
            />
            <div className={styles.gameBoard}>
              <AnimatedNumber
                value={result}
                speedMs={speedMs}
                className={styles.animatedNumber}
              />
              <MultiplierGraph key={chartKey} data={data} speedMs={speedMs} />
            </div>
          </div>
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
