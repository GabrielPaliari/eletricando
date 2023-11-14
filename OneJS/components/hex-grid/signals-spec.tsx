import { h } from "preact";

export type BitArray = Array<number>;
export type TSignals = {
  inputs: Array<BitArray>;
  outputs: Array<BitArray>;
};

export const SignalsSpec = ({ inputs, outputs }: TSignals) => {
  return (
    <div class="">
      <h2 class="text-lg">Entradas</h2>
      <div>
        {inputs.map((inputArray) => (
          <BitArrayDisplay values={inputArray}></BitArrayDisplay>
        ))}
      </div>
      <h2 class="text-lg">Saídas</h2>
      <div>
        {outputs.map((inputArray) => (
          <BitArrayDisplay values={inputArray}></BitArrayDisplay>
        ))}
      </div>
    </div>
  );
};

const BitArrayDisplay = ({ values }: { values: BitArray }) => {
  return (
    <div class="flex">
      {values.map((value) => (
        <Bit value={value}></Bit>
      ))}
    </div>
  );
};

const Bit = ({ value }: { value: number }) => {
  return <div>{value}</div>;
};
