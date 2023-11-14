import { h } from "preact";
import { MenuItem } from "./menu-item";

const placementSystem = require("placementSystem");
const wireSystem = require("wireSystem");

export const BuildingMenu = () => {
  const menuData: [string, () => void][] = [
    ["Fio", () => wireSystem.EnterWireMode()],
    ["LED", () => placementSystem.StartPlacement(1)],
    ["Switch", () => placementSystem.StartPlacement(2)],
    ["Inversor", () => placementSystem.StartPlacement(3)],
    ["Remover", () => placementSystem.StartRemoving()],
  ];

  return (
    <nav className="font-sans bg-indigo-800 flex flex-row mx-auto rounded-lg">
      {menuData.map(([name, onClick]) => {
        return <MenuItem name={name} onClick={onClick}></MenuItem>;
      })}
    </nav>
  );
};
