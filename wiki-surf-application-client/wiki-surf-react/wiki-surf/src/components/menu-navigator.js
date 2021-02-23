import React, {Component, Fragment} from 'react'
import {Grid, GridList, GridListTile} from'@material-ui/core';
import WikiGame from './wiki-game-page';
import {connect} from 'react-redux';
import {linkClicked} from "../redux/actions";
import '../App.css';
import {SCREEN_GAME, SCREEN_MAIN} from "../screen_constants";
import logo from "../logo.svg";
import WikiLinkButton from "./cards/wiki-link-button";
import wikiApi from "../utils/wiki-api";
class WikiNavigator extends Component{
    constructor(props){
        super(props);
        this.state = {

        };
    }
    render(){
        let self = this;
        switch(self.props.wikiEngine.screen){
            case SCREEN_GAME:
                return (
                    <Fragment>
                        <Grid container direction="column" spacing={1}>
                            <Grid item>
                                <WikiGame />
                            </Grid>
                            <Grid item>
                                <Grid container spacing={2}>
                                {
                                    self.props.wikiEngine.wikiContent.pageLinks.map(link =>{
                                    return <WikiLinkButton
                                        word={link.Word}
                                        wordLinkId={link.Id}
                                        key={link.Id}

                                    />})
                                }
                                </Grid>
                            </Grid>
                        </Grid>

                    </Fragment>

                );
            case SCREEN_MAIN:
            default:
                return (
                    <div className="App">
                        <header className="App-header">
                            <img src={logo} className="App-logo" alt="logo"/>
                            <h1 className="App-intro">Welcome to Wiki Surf</h1>
                            <p className="App-intro">Surf through Wikipedia and find the quickest way from one word to another</p>
                        </header>

                        <p className="App-intro">{this.state.apiResponse}</p>
                    </div>
                );
        }

    }
}
const mapStateToProps = state =>{
  return state;
};
export default connect(mapStateToProps,{linkClicked})(WikiNavigator);