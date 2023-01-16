import React from "react";

class CounterClassComponent extends React.Component {
    constructor (props){
        super(props)
        this.state = {
            counter: 0, 
            isHappy : false
        }
        // this.incrementCounter = this.incrementCounter.bind(this)
    }

    // incrementCounter = function() {
    //     this.setState({ counter: this.state.counter + 1 })
    // }

    incrementCounter = () => {
        this.setState({ counter: this.state.counter + 1 })
    }

    toggleHappy = () => {
        this.setState({ isHappy: !this.state.isHappy })
    }

    render() {
        return (
            <>
            <p> I am a class component </p>
            <p> Hello {this.props.name} </p>
            <p> {this.state.counter} </p>
            <button onClick={this.incrementCounter}>+</button>
            { this.state.isHappy && <p>I am happy</p>}
            <button onClick={this.toggleHappy}>toggleHappy</button>
            </>
        )
    }
}

export default CounterClassComponent;