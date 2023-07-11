import { h, render } from "preact";
import { Dom } from "OneJS/Dom";
import { useRef } from "preact/hooks";
import { Style } from "preact/jsx";

const placementSystem = require("placementSystem");
const wireSystem = require("wireSystem");

interface MenuCompProps {
  imgFile: string;
  length: number;
  onClick: () => void;
}

const MenuComp = (props: MenuCompProps) => {
  const defaultInnerStyle: Style = {
    borderRadius: 4,
    paddingBottom: "100%",
    backgroundImage: __dirname + "/assets/" + props.imgFile,
    justifyContent: "Center",
    alignItems: "Center",
    color: "white",
  };

  return (
    <div class="rounded bg-white m-3 w-16" onClick={props.onClick}>
      <div style={defaultInnerStyle}></div>
    </div>
  );
};

const App = () => {
  const ref = useRef<Dom>();

  const navStyle: Style = {
    borderRadius: 8,
  };

  const menuData: [string, () => void][] = [
    ["wire-sprite.png", () => wireSystem.EnterWireMode()],
    ["battery-sprite.png", () => placementSystem.StartPlacement(3)],
    ["resistor-sprite.png", () => placementSystem.StartPlacement(1)],
    ["remove-icon.png", () => placementSystem.StartRemoving()],
  ];
  return (
    <div ref={ref} class="container mt-auto w-screen">
      <nav className="font-sans bg-indigo-800 flex flex-row mx-auto rounded-lg">
        {menuData.map(([imgFile, onClick]) => {
          return (
            <MenuComp
              imgFile={imgFile}
              length={menuData.length}
              onClick={onClick}
            ></MenuComp>
          );
        })}
      </nav>
    </div>
  );
};

render(<App />, document.body);
