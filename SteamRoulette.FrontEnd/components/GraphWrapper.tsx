import { FC } from "react"

import React, { useState } from "react";
import StatusBar from "../components/StatusBar";
import YouLose from "../components/YouLose";

import GameNumbers, { GameState } from "./GameNumbers";

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
    const [finalScore, setFinalScore] = useState<number | null>(null);
    const [gameState, setGameState] = useState<GameState>(GameState.WAITING);
    const handleGameEnd = (finalScore: number) => {
        console.log('Игра завершена. Финальный счёт:', finalScore);
        setFinalScore(finalScore); // Сохраняем финальный счёт в состоянии
    };

    function handleResetGame(): void {
        
    }

    return <>
        <div className="col-span-8 relative">
            {userWon != null ? <YouLose userWon={userWon} /> : null}
            <StatusBar
                loggedIn={isLoggedIn}
                userName={userName}
                points={userPoints}
                userimg={userImg}
            />
            <button onClick={() => { handleGameEnd(10) }}>End</button>
            <GameNumbers
                Score={0} // Текущий счёт
                State={gameState} // Текущее состояние игры
                Acceleration={1} // Ускорение
                TimeToStart={10} // Время до начала игры
                onGameEnd={handleGameEnd} // Передаём функцию в компонент
                onResetGame={handleResetGame} // Передаём функцию для сброса игры
            />
        </div></>
}