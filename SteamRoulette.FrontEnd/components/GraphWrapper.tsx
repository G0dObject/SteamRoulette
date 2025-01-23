import { FC } from "react";
import React from "react";
import StatusBar from "../components/StatusBar";
import YouLose from "../components/YouLose";
import CrashChart from "./CrashChart";
import GamePreparation from "./GamePreparation";

interface GraphWrapperProps {
  userWon: boolean;
  isLoggedIn: boolean;
  userName: string;
  userPoints: number;
  userImg: string;
  result: number;
  isGameStart: boolean;
}

export const GraphWrapper: FC<GraphWrapperProps> = ({
  isGameStart,
  userWon,
  isLoggedIn,
  userName,
  userImg,
  userPoints,
  result,
}) => {
  return (
    <>
      <div className="col-span-8 relative">
        {userWon != null ? <YouLose userWon={userWon} /> : null}
        <StatusBar
          loggedIn={isLoggedIn}
          userName={userName}
          points={userPoints}
          userimg={userImg}
        />
        {isGameStart ? (
          <CrashChart duration={5000} maxMultiplier={10} />
        ) : (
          <GamePreparation />
        )}
      </div>
    </>
  );
};
