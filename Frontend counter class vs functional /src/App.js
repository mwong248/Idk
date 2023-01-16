import logo from './logo.svg';
import CounterFunctionalComponent from './CounterFunctionalComponent';
import CounterClassComponent from './CounterClassComponent';
import './App.css';

function App() {
  return (
    <div className="App">
      {/* <CounterFunctionalComponent /> */}
      <CounterClassComponent name="alice"/>
    </div>
  );
}

export default App;
