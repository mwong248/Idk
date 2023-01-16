import { useState } from 'react';
import './App.css';
import FruitComponent from './Fruit';
import fruits from "./fruits.js";

function App() {

  const [fruitsList, setFruitsList] = useState(fruits)

  const removeFruit = (idOfFruitToRemove) => {
    setFruitsList(fruitsList.filter(fruit => fruit.id !== idOfFruitToRemove))
  }

  function makeRotten(idOfRottenFruit) {
    setFruitsList(fruitsList.map(fruit => {
      if (fruit.id !== idOfRottenFruit) {
        return fruit
      }
      return {
        ...fruit,
        rotten: true,
      }
    }))
  }

  return (
    <div className='App'>
      <ul>
      {fruitsList.map(fruit => {
        return (
          <FruitComponent
            fruit={fruit}
            removeFruit={removeFruit}
            makeRotten={makeRotten}
          />
        )
      })}
      </ul>
    </div>
  );
}

export default App;
