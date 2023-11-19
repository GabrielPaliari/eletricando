import { Dom } from "OneJS/Dom";
import { h } from "preact";
import { useRef } from "preact/hooks";
import { Focusable } from "UnityEngine/UIElements";

export const App = () => {
  const ref = useRef<Dom>();
  return (
    <div ref={ref}>
      <label text="Select something to remove from your suitcase:" />
      <box>
        <toggle name="boots" label="Boots" value={true} />
        <toggle name="helmet" label="Helmet" value={false} />
        <toggle name="cloak" label="Cloak of invisibility" />
      </box>
      <radiobuttongroup>
        <radiobutton name="sword" label="Sword" value={true} />
        <radiobutton name="bow" label="Bow" value={false} />
        <radiobutton name="axe" label="Axe" value={false} />
      </radiobuttongroup>
      <box>
        <button
          name="cancel"
          text="Cancel"
          onClick={(e) => {
            log("Foo");
            (e.currentTarget as Focusable).Blur();
          }}
        />
        <button name="ok" text="OK" />
        <textfield
          onInput={(e) => log(e.newData)}
          onKeyDown={(e) => log(e.keyCode)}
        />
      </box>
    </div>
  );
};
