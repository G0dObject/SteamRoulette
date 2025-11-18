import Bets from "./Bets/Bets";
import Game from "./Game/Game";
import Graph from "./Graph/Graph";
import History from "./History/History";

function GameField() {
    return (
        <div className="m-[30px] flex-1 flex flex-col gap-[10px]">
            <div className={`h-[45%] flex rounded-[10px] overflow-hidden`}>
                <Graph />
                <Game />
            </div>

            <History/>
            <Bets/>
        </div>
    );
}

export default GameField;