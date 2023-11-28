import { h, Fragment } from "preact";
import { useState } from "preact/hooks";
export interface TooltipProps {
  children: any;
  content: string;
}

export const Tooltip = ({ children, content }: TooltipProps) => {
  const [isVisible, setVisible] = useState(false);

  return (
    <>
      <div
        onMouseEnter={() => setVisible(true)}
        onMouseLeave={() => setVisible(false)}
      >
        {children}
        {isVisible && (
          <span
            style={{
              left: "100%",
              top: "50%",
              translate: "10% -50%",
            }}
            class="bg-gray-800 text-white p-2 rounded absolute"
          >
            {content}
          </span>
        )}
      </div>
    </>
  );
};
