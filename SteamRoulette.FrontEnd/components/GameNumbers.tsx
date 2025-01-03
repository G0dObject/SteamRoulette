import React, { useState, useEffect } from 'react';

export interface GameNumbersProps {
    Score: number;
    State: GameState;
    Acceleration: number;
    TimeToStart: number;
    onGameEnd: (finalScore: number) => void; // Пропс для передачи финального счёта
    onResetGame: () => void; // Пропс для сброса игры
}

export enum GameState {
    PREPARING = 'preparing',
    PLAYING = 'playing',
    CRUSHED = 'crushed',
    PENDING = 'pending', // Новое состояние для сброса игры
    WAITING = 'waiting'
}

export const GameNumbers: React.FC<GameNumbersProps> = ({ Score, State, Acceleration, TimeToStart, onGameEnd, onResetGame }) => {
    const [countdown, setCountdown] = useState<number>(TimeToStart); // Используем TimeToStart для инициализации
    const [gameScore, setGameScore] = useState<number>(Score); // Используем Score для инициализации
    const [gameState, setGameState] = useState<GameState>(State);

    // Сброс игры
    const resetGame = () => {
        setCountdown(TimeToStart);
        setGameScore(0);
        setGameState(GameState.PENDING);
        onResetGame(); // Уведомляем родительский компонент о сбросе
    };

    useEffect(() => {
        if (gameState === GameState.PREPARING) {
            const interval = setInterval(() => {
                setCountdown(prev => {
                    if (prev <= 0.1) {
                        clearInterval(interval);
                        setGameState(GameState.PLAYING);
                        return 0;
                    }
                    return parseFloat((prev - 0.1).toFixed(1));
                });
            }, 100);
            return () => clearInterval(interval);
        }
    }, [gameState]);

    useEffect(() => {
        if (gameState === GameState.PLAYING) {
            const interval = setInterval(() => {
                setGameScore(prev => {
                    const newScore = parseFloat((prev + 0.1 * Acceleration).toFixed(1)); // Увеличиваем на десятые доли
                    if (State === GameState.CRUSHED) {
                        clearInterval(interval);
                        onGameEnd(newScore); // Вызываем onGameEnd с финальным счётом
                        setGameState(GameState.PENDING); // Переходим в состояние PENDING
                        return newScore;
                    }
                    return newScore;
                });
            }, 100); // Обновляем каждые 100 мс для плавности
            return () => clearInterval(interval);
        }
    }, [gameState, Acceleration, State, onGameEnd]);

    // Обработка сброса игры
    useEffect(() => {
        if (gameState === GameState.PENDING) {
            resetGame();
        }
    }, [gameState]);

    return (
        <div>
            {gameState === GameState.PREPARING && <div>Countdown: {countdown}</div>}
            {gameState === GameState.PLAYING && <div>Score: {gameScore.toFixed(1)}</div>}
            {gameState === GameState.CRUSHED && <div>Final Score: {gameScore.toFixed(1)}</div>}
            {gameState === GameState.PENDING && <div>Game is pending. Ready to start again!</div>}
        </div>
    );
};

export default GameNumbers;