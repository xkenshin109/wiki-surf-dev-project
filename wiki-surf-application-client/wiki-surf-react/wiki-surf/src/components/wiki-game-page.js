import React, {Component, Fragment} from 'react'
import {Button} from '@material-ui/core';
import '../App.css';
import {connect} from "react-redux";

class WikiGame extends Component{
    constructor(props) {
        super(props);
        this.state = {

        };
    }
    wordClicked(word,id){
        console.log('Word: ', word);
        console.log('Id: ', id);
    }
    render() {
        return (
        <div className="App">
            <header className="App-header">
                <p>{this.props.wikiEngine.wikiContent.pageContent}</p>
            </header>

        </div>)
    };
}
const mapStateToProps = (state) =>{

  return state;
};
export default connect(mapStateToProps)(WikiGame);
