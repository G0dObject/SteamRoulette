import { log } from "console";
import axiosInstance from "./axiosInstance";

async function getInventoryById(userId: string): Promise<SteamItem[]> {
  try {
    const response = await axiosInstance.get(`Inventory/GetUserInventoryById/${userId}`);
    return parseSteamItem(response.data);
  } catch (error) {
    throw error;
  }
}

export async function getInventoryByToken(): Promise<SteamItem[]> {
  try {
    const response = await axiosInstance.get(`Inventory/GetUserInventory`);
    return parseSteamItem(response.data);
  } catch (error) {
    throw error;
  }
}

function parseSteamItem(data: any[]): SteamItem[] {
  return data.map(item => ({
    SteamItemId: item.steamItemId,
    Price: item.steamItemPrice,
    Name: item.steamItemTitle,
    Description: `Item ID: ${item.steamItemId}, User ID: ${item.steamUserId}`,
    ImageUrl: item.steamItemImg,
  }));
}

export interface SteamItem {
  
  SteamItemId: number;
  Price: number;
  Name: string;
  Description: string;
  ImageUrl: string;
}