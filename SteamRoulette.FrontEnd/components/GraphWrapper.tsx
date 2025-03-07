import React, { useEffect, useRef, useState } from "react";
import StatusBar from "../components/StatusBar";
import YouLose from "../components/YouLose";
import GameInterface from "./GameInterface";
import GamePreparation from "./GamePreparation";

interface GraphWrapperProps {
  userWon: boolean;
  isLoggedIn: boolean;
  userName: string;
  userPoints: number;
  userImg: string;
  result: number;
  isGameStart: boolean;
  connection: signalR.HubConnection | null;
}

export const GraphWrapper: React.FC<GraphWrapperProps> = ({
  isGameStart,
  userWon,
  isLoggedIn,
  userName,
  userImg,
  userPoints,
  result,
  connection,
}) => {
  const [value, setValue] = useState<number>(1);

  useEffect(() => {
    if (connection) {``
      connection.on("UpdateNumber", (dataPoint) => {
        console.log(dataPoint);
        setValue(dataPoint);
      });
    }
  }, [connection]);

  return (
    <div className="col-span-8 relative">
      {userWon != null && <YouLose userWon={userWon} />}
      <StatusBar
        loggedIn={isLoggedIn}
        userName={userName}
        points={userPoints}
        userimg={userImg}
      />
      <div className="mt-6">
        {isGameStart ? <GameInterface/> : <GamePreparation />}
      </div>
    </div>
  );
};
