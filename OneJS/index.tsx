import { h, render } from "preact";
import { Dom } from "OneJS/Dom";
import { useRef } from "preact/hooks";

const App = () => {
  const ref = useRef<Dom>();

  return (
    <div ref={ref}>
      <div class="max-w-xl h-full font-sans bg-indigo-600">
        <nav className="flex flex-row">
          {[
            ["Teste", "/dashboard"],
            ["Team", "/team"],
            ["Projects", "/projects"],
            ["", ""],
            ["Reports", "/reports"],
          ].map(([title, url]) => {
            return title ? (
              <div class="basis-1/5 rounded bg-white text-lg p-3 m-3">
                <img src={__dirname + "/assets/battery-sprite.png"} />
              </div>
            ) : (
              <div class="basis-1/5"></div>
            );
          })}
        </nav>
      </div>
    </div>
  );
};

render(<App />, document.body);
