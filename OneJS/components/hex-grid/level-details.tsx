import { Style } from "preact/jsx";
import { h } from "preact";
import { Debug, ScaleMode } from "UnityEngine";
import { SignalsSpec, TSignals } from "./signals-spec";

export type ILevelDetails = {
  id: string;
  title: string;
  description: string;
  componentImg: string;
} & TSignals;

export const LevelDetails = ({
  id,
  title,
  description,
  componentImg,
  ...signals
}: ILevelDetails) => {
  return (
    <div class="rounded-l-lg bg-teal-700 mt-3 p-4 max-w-sm ml-auto">
      <h1 class="text-2xl">{title}</h1>
      <div class="w-20">
        <Img fileUrl={componentImg}></Img>
      </div>
      <p class="text-base">{description}</p>
      <SignalsSpec {...signals}></SignalsSpec>
      <div
        class="rounded-lg bg-indigo-500"
        onClick={() => Debug.Log("Jogar level" + id)}
      >
        <p class="text-lg text-center">Jogar Level</p>
      </div>
    </div>
  );
};

export const Img = ({ fileUrl }: { fileUrl: string }) => {
  const src = __dirname + "../../../assets/" + fileUrl;
  const defaultInnerStyle: Style = {
    width: "100%",
    paddingBottom: "100%",
    backgroundImage: src,
    unityBackgroundScaleMode: "ScaleAndCrop",
  };

  return <div style={defaultInnerStyle}></div>;
};
