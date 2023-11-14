import { Style } from "preact/jsx";
import { h } from "preact";

export interface MenuItemProps {
  name: string;
  onClick: () => void;
}

export const MenuItem = (props: MenuItemProps) => {
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
