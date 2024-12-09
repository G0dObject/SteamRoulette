import axiosInstance from "./axiosInstance";

async function getInventoryById(userId: string): Promise<SteamItem[]> {
  try {
    const response = await axiosInstance.get(`/users/${userId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
}

interface SteamItem {
  Price: number;
  Name: string;
  Description: string;
  ImageUrl: string;
}
