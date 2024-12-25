import { FC } from "react"

import React, { useState, useEffect, useRef } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

import WelcomeLogin from "../components/WelcomeLogin";
import StatusBar from "../components/StatusBar";
import NumberInput from "../components/NumberInput";
import MultiplierGraph from "./Rocket";
import UserInventory from "../components/UserInventory";
import Ranking from "../components/Ranking";
import Chat from "../components/Chat";
import AnimatedNumber from "../components/AnimatedNumber";
import YouLose from "../components/YouLose";

import styles from "../styles/Root.module.scss";
import authStore from "../src/store/AuthStore";
import { start } from "repl";
import Rocket from "./Rocket";


interface GraphWrapperProps {
    userWon: boolean;
    isLoggedIn: boolean;
    userName: string;
    userPoints: number;
    userImg: string;
    result: number;
}


export const GraphWrapper: FC<GraphWrapperProps> = ({ userWon, isLoggedIn, userName, userImg, userPoints, result }) => {

    const [isGameRunning, setIsGameRunning] = useState(true);
    const handleStart = () => {
        console.log("Анимация запущена!");
    };
    return <> <div className="col-span-8 relative">
        {userWon != null ? <YouLose userWon={userWon} /> : null}
        <StatusBar
            loggedIn={isLoggedIn}
            userName={userName}
            points={userPoints}
            userimg={userImg}
        />

        <AnimatedNumber
            value={result}
            speedMs={500}
            className={styles.animatedNumber}
        />
        <Rocket

        />

    </div></>
}