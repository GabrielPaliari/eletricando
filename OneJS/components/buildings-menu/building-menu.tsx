import { h } from "preact";
import { MenuItem } from "./menu-item";
import { List } from "System/Collections/Generic";
import { useState } from "preact/hooks";
import { Tooltip } from "components/shared/tooltip";

const placementSystem = require("placementSystem");
const wireSystem = require("wireSystem");

interface PlaceableComponentSO {
  Name: string;
  ID: number;
  menuImage: any;
}
const getComponentFunction = (
  id: number,
  setSelected: (id: number) => void
) => {
  let componentFunction;
  switch (id) {
    case -1:
      componentFunction = () => placementSystem.StartRemoving();
      break;
    case 0:
      componentFunction = () => wireSystem.EnterWireMode();
      break;
    default:
      componentFunction = () => placementSystem.StartPlacement(id);
      break;
  }
  return () => {
    setSelected(id);
    componentFunction();
  };
};

export const BuildingMenu = () => {
  const [selected, setSelected] = useState(-100);
  const componentsDatabase: List<PlaceableComponentSO> =
    placementSystem.database?.objectsData;
  const componentsData: [number, string, () => void, any][] = [];
  componentsDatabase.ForEach(({ Name, ID, menuImage }) =>
    componentsData.push([
      ID,
      Name,
      getComponentFunction(ID, setSelected),
      menuImage,
    ])
  );

  const menuData: [number, string, () => void, any][] = componentsData;

  return (
    <div class="bg-indigo-800 rounded-lg w-20">
      {menuData.map(([id, name, onClick, image]) => {
        return (
          <Tooltip content={name}>
            <MenuItem
              name={name}
              onClick={onClick}
              image={image}
              isSelected={selected === id}
            ></MenuItem>
          </Tooltip>
        );
      })}
    </div>
  );
};
