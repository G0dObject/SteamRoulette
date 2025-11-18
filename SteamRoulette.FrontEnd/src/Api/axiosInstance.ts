import axios from "axios";
import authStore from "../store/AuthStore";
const baseUrl = import.meta.env.VITE_API_URL;




const axiosInstance = axios.create({
  baseURL: baseUrl + "/api/",
  headers: {
    "Content-Type": "application/json",
    'Authorization': 'Bearer ' + authStore.token
  },
});

export default axiosInstance;
