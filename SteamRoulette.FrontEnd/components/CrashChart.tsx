import React, { useRef, useEffect, useState } from "react";

interface CrashChartProps {
  duration?: number; // Длительность анимации (в миллисекундах)
  maxMultiplier?: number; // Максимальный множитель
}

const CrashChart: React.FC<CrashChartProps> = ({
  duration = 5000,
  maxMultiplier = 10,
}) => {
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const runnerRef = useRef<HTMLImageElement>(null);
  const [currentMultiplier, setCurrentMultiplier] = useState(0);
  const startTimeRef = useRef(Date.now());

  // Функция для отрисовки графика
  const drawChart = () => {
    const canvas = canvasRef.current;
    if (!canvas) return;

    const ctx = canvas.getContext("2d");
    if (!ctx) return;

    const width = canvas.width;
    const height = canvas.height;
    const padding = 40; // Отступы для осей

    // Очистка canvas
    ctx.clearRect(0, 0, width, height);

    // Заливка фона серым цветом
    ctx.fillStyle = "#202630";
    ctx.fillRect(0, 0, width, height);

    // Оси
    ctx.strokeStyle = "#ccc";
    ctx.beginPath();
    ctx.moveTo(padding, height - padding);
    ctx.lineTo(width - padding, height - padding); // Ось X
    ctx.moveTo(padding, height - padding);
    ctx.lineTo(padding, padding); // Ось Y
    ctx.stroke();

    // Подписи осей
    ctx.fillStyle = "#ffffff";
    ctx.font = "20px Arial";
    ctx.fillText("0", padding - 20, height - padding + 20);

    // Текущее время анимации
    const elapsedTime = Date.now() - startTimeRef.current;
    const progress = Math.min(elapsedTime / duration, 1); // Прогресс от 0 до 1

    // Вычисление текущего множителя
    const multiplier = progress * maxMultiplier;
    setCurrentMultiplier(multiplier);

    // Отрисовка линии графика
    ctx.strokeStyle = "#ffffff"; // Красный цвет линии
    ctx.lineWidth = 2;
    ctx.beginPath();
    ctx.moveTo(padding, height - padding);

    // Анимация линии
    for (let x = padding; x <= width - padding; x++) {
      const xProgress = (x - padding) / (width - 2 * padding);
      const yProgress = xProgress * progress;
      const y = height - padding - yProgress * (height - 2 * padding);
      ctx.lineTo(x, y);
    }

    ctx.stroke();

    // Позиция точки и анимации (фиксированная по X, двигается по Y)
    const pointX = width - padding; // Фиксированная позиция по X (конец линии)
    const pointY = height - padding - progress * (height - 2 * padding); // Двигается по Y

    // Отрисовка точки на конце линии
    ctx.fillStyle = "#FFFFFFFF"; // Красный цвет точки
    ctx.beginPath();
    ctx.arc(pointX, pointY, 5, 0, Math.PI * 2); // Точка радиусом 5px
    ctx.fill();

    // Позиционирование GIF-анимации
    if (runnerRef.current) {
      const runnerSize = 50; // Размер GIF
      const offset = 20; // Смещение выше точки
      const runnerY = pointY - offset * 3; // Смещение выше точки

      runnerRef.current.style.position = "absolute";
      runnerRef.current.style.right = `${runnerSize - 30}px`;
      runnerRef.current.style.top = `${runnerY}px`;
    }

    ctx.fillStyle = "#FFFFFFFF";
    ctx.font = "20px Arial";
    ctx.fillText(
      `Multiplier: ${multiplier.toFixed(2)}x`,
      width / 2 - 60,
      padding + 30
    );

    if (progress < 1) {
      requestAnimationFrame(drawChart);
    }
  };

  useEffect(() => {
    startTimeRef.current = Date.now();
    drawChart();
  }, [duration, maxMultiplier]);

  return (
    <div className="flex flex-col justify-center min-h-60 bg-gray-100 relative">
      <canvas
        ref={canvasRef}
        width={800}
        height={400}
        className="border border-gray-300 shadow-lg"
      />

      <img
        ref={runnerRef}
        src={"/Runner.gif"}
        alt="Running"
        style={{ height: "50px" }}
      />
    </div>
  );
};

export default CrashChart;
