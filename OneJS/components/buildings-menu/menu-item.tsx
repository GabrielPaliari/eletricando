import { Style } from "preact/jsx";
import { h } from "preact";
import { Tooltip } from "components/shared/tooltip";

export interface MenuItemProps {
  name: string;
  onClick: () => void;
  image: any;
  isSelected?: boolean;
}

export const MenuItem = (props: MenuItemProps) => {
  const defaultInnerStyle: Style = {
    borderRadius: 4,
    height: 40,
    backgroundImage: props.image,
    unityBackgroundScaleMode: "ScaleToFit",
  };

  const borderStyle: Style = {
    borderColor: props.isSelected ? "white" : "transparent",
    borderWidth: 4,
  };

  return (
    <div
      style={borderStyle}
      class="rounded bg-teal-700 m-1 p-2"
      onClick={props.onClick}
    >
      <div style={defaultInnerStyle}></div>
    </div>
  );
};
