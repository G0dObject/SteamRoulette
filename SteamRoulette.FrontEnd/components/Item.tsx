import { SteamItem } from "@/src/Api/inventory";
import { FC, useState } from "react";

interface ItemProps {
    item: SteamItem;
}

export const Item: FC<ItemProps> = ({ item }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [multiplier, setMultiplier] = useState(1);

    const openModal = () => setIsModalOpen(true);
    const closeModal = () => setIsModalOpen(false);

    const incrementMultiplier = () => setMultiplier(prev => prev + 1);
    const decrementMultiplier = () => setMultiplier(prev => Math.max(1, prev - 1));

    return (
        <>
            {/* Основной блок предмета */}
            <div
                key={item.SteamItemId}
                className="bg-gray-700 p-2 rounded text-center cursor-pointer"
                onClick={openModal}
            >
                <img
                    src={item.ImageUrl}
                    alt={item.Name}
                    className="w-full h-24 object-cover mb-2"
                />
                <span className="text-white">{item.Name}</span>
                <span className="text-gray-300 block mt-1">${item.Price}</span>
            </div>

            {/* Модальное окно */}
            {isModalOpen && (
                <div
                    className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50" // Добавлен z-50 для оверлея
                    onClick={closeModal} // Закрытие модального окна при клике на оверлей
                >
                    <div
                        className="bg-gray-800 p-4 rounded-lg w-80 relative z-50" // Добавлен z-50 для самого окна
                        onClick={e => e.stopPropagation()} // Предотвращаем закрытие при клике внутри модального окна
                    >
                        {/* Кнопка закрытия */}
                        <button
                            className="absolute top-2 right-2 text-white"
                            onClick={closeModal}
                        >
                            &times;
                        </button>

                        {/* Миниатюра изображения */}
                        <div className="flex justify-center mb-4">
                            <img
                                src={item.ImageUrl}
                                alt={item.Name}
                                className="w-16 h-16 object-cover rounded"
                            />
                        </div>

                        {/* Название предмета */}
                        <div className="text-center text-white mb-4">
                            <span>{item.Name}</span>
                        </div>

                        {/* Кнопки управления множителем */}
                        <div className="flex justify-center items-center space-x-4">
                            <button
                                className="bg-red-500 text-white px-4 py-2 rounded"
                                onClick={decrementMultiplier}
                            >
                                -
                            </button>
                            <span className="text-white text-xl">{multiplier}</span>
                            <button
                                className="bg-green-500 text-white px-4 py-2 rounded"
                                onClick={incrementMultiplier}
                            >
                                +
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </>
    );
};