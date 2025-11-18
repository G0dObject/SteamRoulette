import React, { useState, useEffect, useRef } from 'react';
import styled, { keyframes } from 'styled-components';

// Контейнер для поля
const Field = styled.div`
    width: 100%; // Занимает всю ширину родительского контейнера
    height: 100%; // Занимает всю высоту родительского контейнера
    max-width: 370px; // Максимальная ширина для больших экранов
    max-height: 360px; // Максимальная высота для больших экранов
    aspect-ratio: 370 / 360; // Сохраняем пропорции
    background-color: #24202d;
    position: relative;
    border: 15px solid #1C1925;
    overflow: hidden;

    @media (max-width: 768px) {
        max-width: 300px;
        max-height: 300px;
    }

    @media (max-width: 480px) {
        max-width: 250px;
        max-height: 250px;
    }
`;

// Анимация для луча
const rayAnimation = keyframes`
    from {
        transform: rotate(90deg);
    }
    to {
        transform: rotate(55deg);
    }
`;

// Луч
const Ray = styled.div`
    position: absolute;
    bottom: 0;
    left: 0;
    width: 2px;
    height: 150%;
    background: #8F89A5;
    transform-origin: bottom left;
    animation: ${rayAnimation} 3s linear forwards;
`;

// Анимация для точек сетки
const gridAnimation = keyframes`
    from {
        transform: translateY(-500%);
    }
    to {
        transform: translateY(150%);
    }
`;

// Точка сетки
const GridPoint = styled.div.attrs<{ speed: number }>((props) => ({
    style: {
        animationDuration: `${props.speed}s`,
    },
}))<{ speed: number }>`
    position: absolute;
    width: 4px;
    height: 4px;
    background-color: rgba(143, 137, 165, 0.5);
    border-radius: 50%;
    animation: ${gridAnimation} linear infinite;
    will-change: transform; // Оптимизация для плавной анимации
`;

// Текущий множитель
const Multiplier = styled.div`
    position: absolute;
    top: 30%;
    left: 50%;
    transform: translate(-50%, -50%);
    font-size: 40px;
    font-weight: bold;
    color: #fff;
    transition: opacity 0.5s ease-in-out;

    @media (max-width: 768px) {
        font-size: 20px;
    }

    @media (max-width: 480px) {
        font-size: 16px;
    }
`;

const Graph: React.FC = () => {
    const [multiplier, setMultiplier] = useState(1.0);
    const [animationSpeed, setAnimationSpeed] = useState(1); // Начальная скорость анимации
    const rayRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const interval = setInterval(() => {
            setMultiplier((prev) => prev * 1.001);
        }, 10);

        return () => clearInterval(interval);
    }, []);

    // Генерация точек сетки
    const gridPoints = [];

    const rows = 12; // Количество строк
    const cols = 12; // Количество столбцов
    const horizontalSpacing = 100 / cols; // Процентное расстояние между точками по горизонтали
    const verticalSpacing = 100 / rows; // Процентное расстояние между точками по вертикали

    for (let row = 0; row < rows; row++) {
        for (let col = 0; col < cols; col++) {
            const x = col * horizontalSpacing;
            const y = row * verticalSpacing;
            gridPoints.push(
                <GridPoint
                    key={`${row}-${col}`}
                    style={{ left: `${x}%`, top: `${y}%` }} // Используем проценты
                    speed={animationSpeed} // Передаем текущую скорость анимации
                />
            );
        }
    }

    // Пример изменения скорости анимации через 5 секунд
    useEffect(() => {
        const timeout = setTimeout(() => {
            setAnimationSpeed(0.5); // Устанавливаем новую скорость анимации
        }, 5000); // Через 5 секунд

        return () => clearTimeout(timeout);
    }, []);

    return (
        <Field className="flex-1">
            {gridPoints}
            <Ray ref={rayRef} />
            <Multiplier>{multiplier.toFixed(2)}x</Multiplier>
        </Field>
    );
};

export default Graph;