import { SteamItem } from "@/src/Api/inventory";
import { FC, useState } from "react";

interface ItemProps {
  item: SteamItem;
}

export const Item: FC<ItemProps> = ({ item }) => {
  const [multiplier, setMultiplier] = useState(1);

  const incrementMultiplier = () => setMultiplier((prev) => prev + 1);
  const decrementMultiplier = () =>
    setMultiplier((prev) => Math.max(1, prev - 1));

  return (
    <>
      {/* Основной блок предмета */}
      <div
        key={item.SteamItemId}
        className=" bg-gray-700  h-fit p-2 rounded text-center cursor-pointer"
      >
        <img
          src={item.ImageUrl}
          alt={item.Name}
          className="w-full h-24 object-cover mb-2"
        />
        <span className="text-white">{item.Name}</span>
        <span className="text-gray-300 block mt-1">${item.Price}</span>
      </div>
    </>
  );
};
