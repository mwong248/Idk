import { useState } from "react";

const CounterFunctionalComponent = () => {
    const [counterNumber, setCounterNumber] = useState(0)
  
    return (
      <>
      <p> I am a fucntional component </p>
      <p> {counterNumber} </p>
      <button onClick={() => setCounterNumber(counterNumber + 1)}></button>
      </>
    )
  }

  export default CounterFunctionalComponent;