import { Dom } from "OneJS/Dom";
import { BuildingMenu } from "components/buildings-menu/building-menu";
import { h, render } from "preact";
import { useRef } from "preact/hooks";

const App = () => {
  return <BuildingMenu></BuildingMenu>;
};

render(<App />, document.body);
