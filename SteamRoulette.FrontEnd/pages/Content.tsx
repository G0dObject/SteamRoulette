import GameField from "../components/GameField";
import Sidebar from "../components/SideBar/SideBar";
import Chat from "../components/Chat/Chat";

function Content() {
  return (
    <div className="h-screen flex flex-col pt-[40px] p-[10px]">
      <div className="flex flex-1 gap-[5px]">
        <Sidebar />
        <div className="flex-1 flex flex-col gap-[10px] bg-[#262230]">
          <GameField />
        </div>
        <Chat />
      </div>
    </div>
  );
}

export default Content;
