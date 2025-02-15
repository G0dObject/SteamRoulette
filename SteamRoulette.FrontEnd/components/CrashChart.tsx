import React, { useEffect, useState } from "react";
import { Line } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  ChartOptions,
} from "chart.js";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

interface LiveChartProps {
  newValue: number; // Новое значение, передаваемое извне
}

const CrushChart: React.FC<LiveChartProps> = ({ newValue }) => {
  const [data, setData] = useState<number[]>([1]); // Начальное значение (0, 0)

  useEffect(() => {
    setData((prevData) => [...prevData, newValue]);
  }, [newValue]);

  const chartData = {
    labels: data,
    datasets: [
      {
        data: data,
        borderColor: "rgba(75, 192, 192, 1)",
        backgroundColor: "rgba(75, 192, 192, 0.2)",
        pointRadius: 5,
        pointBackgroundColor: "rgba(75, 192, 192, 1)",
      },
    ],
  };

  const options: ChartOptions<"line"> = {
    responsive: true,
    scales: {
      x: {
        type: "linear",
        position: "bottom",
        title: {
          display: true,
          text: "Время (десятые доли секунды)",
        },
        grid: {
          display: true,
        },
      },
      y: {
        beginAtZero: true,
        title: {
          display: true,
          text: "Значение",
        },
        grid: {
          display: true,
        },
      },
    },
    plugins: {
      tooltip: {
        callbacks: {
          label: (context) => {
            return `Значение: ${context.raw}`;
          },
        },
      },
      legend: {
        display: false, // Полностью отключаем легенду
      },
    },
  };

  return (
    <div style={{ width: "100%", height: "400px" }}>
      <Line data={chartData} options={options} />
    </div>
  );
};

export default CrushChart;
