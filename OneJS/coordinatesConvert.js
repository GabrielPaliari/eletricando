const getNextCoordinates = ({ q, r, s }, dir, signal) => {
  if (dir == "q") {
    return {
      q,
      s: s + signal,
      r: r - signal,
    };
  }

  if (dir == "s") {
    return {
      q: q - signal,
      s,
      r: r + signal,
    };
  }

  if (dir == "r") {
    return {
      q: q + signal,
      s: s - signal,
      r,
    };
  }
};

function getDirAndSignal({ q, r, s }, dir, signal, index) {
  if (s == 0) {
    return ["q", signal * -1];
  }

  if (r == 0) {
    return ["s", signal * -1];
  }

  if (q == 0) {
    return ["r", signal * -1];
  }
};

function scalarToCubeCoordinates(scalar) {
  let coordinates = { q: 1, r: -1, s: 0 };

  let dir = "r";
  let signal = 1;

  for (let i = 2; i <= scalar; i++) {
    [dir, signal, coordinates] = getDirAndSignal(coordinates, dir, signal, i);
    console.log(dir, signal, coordinates);
  }

  return coordinates;
}



const pos = scalarToCubeCoordinates(7);
console.log(`${i}: q:${pos.q}, r:${pos.r}, s:${pos.s}`);

