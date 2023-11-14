import { Dom } from "OneJS/Dom";
import { BuildingMenu } from "components/buildings-menu/building-menu";
import { h, render } from "preact";
import { useRef } from "preact/hooks";

const App = () => {
  const ref = useRef<Dom>();

  return (
    <div ref={ref} class="container">
      <BuildingMenu></BuildingMenu>
    </div>
  );
};

render(<App />, document.body);
