import Inventory from "./Inventory";

interface InventoryItem {
  id: number;
  name: string;
  image: string;
  price: number;
}
const UserInventory: React.FC = () => {
  const items: InventoryItem[] = [
    {
      id: 1,
      name: "AK-47 | Redline",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f ",
      price: 2500,
    },
    {
      id: 2,
      name: "AWP | Dragon Lore",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 5000,
    },
    {
      id: 3,
      name: "M4A4 | Howl",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 3000,
    },
    {
      id: 4,
      name: "Desert Eagle | Blaze",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1500,
    },
    {
      id: 5,
      name: "Glock-18 | Fade",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1000,
    },
    {
      id: 6,
      name: "USP-S | Kill Confirmed",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1200,
    },
    {
      id: 7,
      name: "USP-S | Kill Confirmed",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1200,
    },
    {
      id: 8,
      name: "USP-S | Kill Confirmed",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1200,
    },
    {
      id: 9,
      name: "USP-S | Kill Confirmed",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1200,
    },
    {
      id: 10,
      name: "USP-S | Kill Confirmed",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1200,
    },
    {
      id: 11,
      name: "USP-S | Kill Confirmed",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1200,
    },
    {
      id: 12,
      name: "USP-S | Kill Confirmed",
      image:
        "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
      price: 1200,
    },
  ];

  return (
    <div className="flex flex-col items-center mb-2">
      <Inventory items={items} />
    </div>
  );
};

export default UserInventory;
