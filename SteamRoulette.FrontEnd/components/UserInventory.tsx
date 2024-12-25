import { FC } from "react";
import Inventory from "./Inventory";
import { SteamItem } from "@/src/Api/inventory";



interface UserInventoryProps{
  items: SteamItem[]
}
const UserInventory: FC<UserInventoryProps> = ({items}) => {

  return (
    <div className="flex flex-col items-center mb-2">
      <Inventory items={items} />
    </div>
  );
};

export default UserInventory;
