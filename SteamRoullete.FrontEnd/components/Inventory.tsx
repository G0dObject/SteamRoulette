import React from "react";

interface InventoryItem {
  id: number;
  name: string;
  image: string;
  price: number;
}

interface InventoryProps {
  items: InventoryItem[];
}

const Inventory: React.FC<InventoryProps> = ({ items }) => {
  return (
    <div className="bg-gray-800 p-4 rounded-lg shadow-lg w-full max-h-96 overflow-y-auto">
      <div className="flex items-center mb-4">
        <svg
          className="fill-white w-6 h-6 mr-1"
          fill="#000000"
          height="800px"
          width="800px"
          version="1.1"
          id="Layer_1"
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 511.999 511.999"
        >
          <g>
            <g>
              <path d="M484.666,6.718c-0.698-3.066-3.006-5.51-6.027-6.38c-3.016-0.87-6.274-0.031-8.499,2.192l-1.43,1.43    c-36.409,36.409-77.803,67.347-123.033,91.954l-6.102,3.319c-0.728,0.396-1.394,0.893-1.98,1.479L206.38,231.926l-31.12-31.12    c-3.372-3.371-8.838-3.372-12.212,0c-3.372,3.372-3.372,8.839,0,12.21l31.12,31.12L32.743,405.565    c-6.841,6.841-10.609,15.936-10.609,25.611c0,9.674,3.767,18.77,10.608,25.611l44.603,44.603    c6.841,6.841,15.937,10.609,25.611,10.609c9.675,0,18.772-3.767,25.612-10.609l161.427-161.428l37.42,37.42    c1.687,1.685,3.897,2.529,6.106,2.529c2.209,0,4.42-0.843,6.106-2.529c3.372-3.372,3.372-8.84,0-12.21l-37.42-37.42    l126.268-126.269c25.113-25.113,43.547-56.429,53.311-90.561C491.474,77.054,492.469,41.02,484.666,6.718z M231.006,231.722    l12.794,12.794c1.687,1.685,3.897,2.529,6.106,2.529c2.209,0,4.42-0.843,6.106-2.529c3.372-3.372,3.372-8.839,0-12.21    l-12.794-12.794l12.95-12.95l12.794,12.794c1.687,1.685,3.897,2.529,6.106,2.529c2.209,0,4.42-0.843,6.106-2.529    c3.372-3.372,3.372-8.839,0-12.21l-12.794-12.794l12.95-12.95l12.794,12.794c1.687,1.685,3.897,2.529,6.106,2.529    c2.209,0,4.42-0.843,6.106-2.529c3.372-3.372,3.372-8.839,0-12.21l-12.794-12.794l55.384-55.384l5.006-2.723    c32.385-17.619,62.858-38.392,91.038-62.004c-1.844,48.207-21.789,94.33-56.193,128.733L270.52,296.065l-51.928-51.929    L231.006,231.722z M116.355,489.18c-3.578,3.579-8.338,5.55-13.4,5.55c-5.061,0-9.82-1.971-13.4-5.55l-44.603-44.603    c-3.579-3.579-5.55-8.339-5.55-13.401s1.971-9.822,5.55-13.401l3.361-3.361l71.403,71.403L116.355,489.18z M260.788,344.748    l-22.694-22.694c-3.373-3.372-8.839-3.372-12.212,0c-3.372,3.372-3.372,8.839,0,12.21l22.694,22.694l-13.561,13.561l-37.66-37.659    c-3.373-3.372-8.839-3.372-12.212,0c-3.372,3.372-3.372,8.839,0,12.21l37.66,37.66l-13.561,13.561l-44.865-44.864    c-3.373-3.372-8.839-3.372-12.212,0c-3.372,3.372-3.372,8.839,0,12.21l44.865,44.865l-13.56,13.56l-44.864-44.864    c-3.373-3.372-8.839-3.372-12.212,0c-3.372,3.372-3.372,8.84,0,12.21l44.865,44.865l-13.561,13.561l-44.865-44.865    c-3.373-3.372-8.839-3.372-12.212,0c-3.372,3.372-3.372,8.839,0,12.21l44.865,44.865l-13.561,13.561l-71.403-71.403    l145.854-145.856l71.404,71.404L260.788,344.748z M465.183,106.172c-8.958,31.32-25.875,60.055-48.919,83.099L289.996,315.542    l-7.265-7.265l118.254-118.254c41.225-41.225,63.405-97.889,61.25-156.038c2.844-2.59,5.67-5.2,8.461-7.851    C474.474,52.807,472.617,80.186,465.183,106.172z" />
            </g>
          </g>
        </svg>
        <h1 className="font-bold text-xl"> Inventory</h1>
      </div>
      <div className="grid grid-cols-3 gap-2">
        {items.map((item) => (
          <div key={item.id} className="bg-gray-700 p-2 rounded text-center">
            <img
              src={item.image}
              alt={item.name}
              className="w-full h-24 object-cover mb-2"
            />
            <span className="text-white">{item.name}</span>
            <span className="text-gray-300 block mt-1">${item.price}</span>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Inventory;
