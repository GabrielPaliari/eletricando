import { h, render } from "preact";
import { Dom } from "OneJS/Dom";
import { useRef } from "preact/hooks";
import { Style } from "preact/jsx";

const placementSystem = require("placementSystem");
const wireSystem = require("wireSystem");

interface MenuCompProps {
  name: string;
  onClick: () => void;
}

const MenuComp = (props: MenuCompProps) => {
  const defaultInnerStyle: Style = {
    borderRadius: 4,
    justifyContent: "Center",
    alignItems: "Center",
    color: "white",
    padding: 20,
    fontSize: 20,
  };

  return (
    <div class="rounded bg-teal-700 m-3" onClick={props.onClick}>
      <div style={defaultInnerStyle}>{props.name}</div>
    </div>
  );
};

const App = () => {
  const ref = useRef<Dom>();

  const navStyle: Style = {
    borderRadius: 8,
  };

  const menuData: [string, () => void][] = [
    ["Fio", () => wireSystem.EnterWireMode()],
    ["LED", () => placementSystem.StartPlacement(1)],
    ["Switch", () => placementSystem.StartPlacement(2)],
    ["Remover", () => placementSystem.StartRemoving()],
  ];
  return (
    <div ref={ref} class="container mt-auto w-screen">
      <nav className="font-sans bg-indigo-800 flex flex-row mx-auto rounded-lg">
        {menuData.map(([name, onClick]) => {
          return <MenuComp name={name} onClick={onClick}></MenuComp>;
        })}
      </nav>
    </div>
  );
};

render(<App />, document.body);
