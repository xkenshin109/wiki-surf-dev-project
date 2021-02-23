import './App.css';
import React, {Component} from 'react';
import {connect} from 'react-redux';
import WikiNavigator from './components/menu-navigator';
import {SCREEN_GAME} from "./screen_constants";
import Container from'@material-ui/core/Container';
import {newSession, gameStart, gameContent, navigate, linkClicked} from "./redux/actions";
import WikiMenu from './components/menu';
import wikiApi from './utils/wiki-api'
class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            game:{},
            screen:'main',
            gameStarted: false
        };
    }
    newSession (){
        let self = this;
        console.log('Obtaining new session');
        wikiApi.newSession()
            .then(res=>{
                self.props.newSession(res);
            });
    };
    startGame (){
        let self = this;
        console.log('starting game');
        if(self.state.gameStarted){
            //game already started
            return;
        }
        wikiApi.startGame(self.props.session.WikiSessionId)
            .then(res=>{
                self.props.gameContent(res);
                self.props.gameStart(true);
                self.props.navigate(SCREEN_GAME);
            });
    };
    componentDidMount() {
        let self = this;
        if(self.props.session.WikiSessionId === ''){
            this.newSession();
        }
    }
    enableStartButton(){
        let self = this;
        return self.props.session.WikiSessionId === '' && !self.props.wikiEngine.gameStarted;
    }
    render() {
        let self = this;
        return (
          <Container maxWidth="lg">
                <WikiMenu />
            <WikiNavigator />
            <button onClick={self.startGame.bind(self)} disabled={self.enableStartButton()}>Start Game</button>
          </Container>
        );
    }
}
const mapDispatchToProps = state =>{
    //const {session} = getSessionState(state);
    return state;
};
export default connect(mapDispatchToProps,
    {newSession, gameStart, gameContent, navigate, linkClicked})(App);
