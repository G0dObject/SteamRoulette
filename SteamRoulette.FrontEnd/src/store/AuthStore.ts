// src/store/AuthStore.ts
import { makeAutoObservable } from "mobx";
import { jwtDecode } from "jwt-decode";

class AuthStore {
  token: string | null = null;
  img: string | null = null;
  name: string | null = null;

  constructor() {
    makeAutoObservable(this);
    this.loadToken(); // Загружаем токен при инициализации
  }

  setToken(token: string) {
    console.log(jwtDecode(token));
    this.token = token;
    localStorage.setItem("authToken", token);

    this.setName();
    this.setImg();
  }

  setName() {
    var name = (jwtDecode(this.token as string) as UserClaims)[
      "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
    ];
    this.name = name;
    localStorage.setItem("Username", name);
  }

  setImg() {
    var img = (jwtDecode(this.token as string) as UserClaims)[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata"
    ];
    this.img = img;
    localStorage.setItem("UserImg", img);
  }

  clearToken() {
    this.token = "";
    localStorage.removeItem("authToken");
  }

  loadToken() {
    const token = localStorage.getItem("authToken");
    if (token) {
      this.token = token;
      this.setName();
      this.setImg();
    }
  }
}

const authStore = new AuthStore();
export default authStore;
