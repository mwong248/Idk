const FruitComponent = ({ fruit, removeFruit, makeRotten }) => {
    return (
        <>
        <li onClick={() => removeFruit(fruit.id)}
        className={fruit.rotten ? 'rotten' : ''} >
          {fruit.name}
        </li>
        <button onClick={() => makeRotten(fruit.id)}>
          Rotten!
        </button>
      </>
    )
}

export default FruitComponent;