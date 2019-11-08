import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name

    constructor(props) {
        super(props);
        this.state = { inputValue: '', receivedMessage: '' }
        this.handleSubmit = this.handleSubmit.bind(this)
        this.updateValue = this.updateValue.bind(this)
        this.getMessages = this.getMessages.bind(this)
        this.updateName = this.updateName.bind(this)
    }

    handleSubmit(event) {
        event.preventDefault()
        fetch("api/Message/SendMessage?message=" + this.state.nameValue + ": " + this.state.inputValue)
        this.setState({
            inputValue: ''
        })
    }

    updateValue(event) {
        this.setState({
            inputValue: event.target.value
        })
    }

    updateName(event) {
        this.setState({
            nameValue: event.target.value
        })
    }

    getMessages = () => {
        fetch("api/Message/Listen")
            .then(response => response.json())
            .then(data => {
                if (data != null) {
                    this.setState({ receivedMessage: this.state.receivedMessage + data + "\n" })
                }
            })
    }

    async componentDidMount() {
        this.interval = setInterval(async () => await this.getMessages(), 25);
        //await this.getMessages()
    }

    render() {
        return (
            <div>
                <form>
                    Name: <input id="inputBox" type="text" value={this.state.nameValue} onChange={this.updateName} size="25" />
                </form>

                <form>
                    <textarea value={this.state.receivedMessage} rows="10" cols="58" readOnly="true" />
                </form>

                <form onSubmit={this.handleSubmit}>
                    <input id="inputBox" type="text" value={this.state.inputValue} onChange={this.updateValue} size="50" />
                    <input type="submit" value="Send" />
                </form>

            </div>
        );
    }
}
