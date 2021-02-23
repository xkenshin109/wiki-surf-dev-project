import React, {Component} from 'react';
import {Typography, Button, Card, CardContent, CardActions} from '@material-ui/core';
import {connect} from "react-redux";
import {getWikiSessionId} from "../../redux/selectors";
import wikiApi from '../../utils/wiki-api';
import {linkClicked} from "../../redux/actions";
class WikiLinkButton extends Component{
    constructor(props){
        super(props);
        this.state = {
          word: props.word,
          wordLinkId: props.wordLinkId,
          key: props.key,
          loaded:false
        };
    }
    performClick = async (parent) =>{
        let self = parent;
        let res = await wikiApi.linkClicked(self.state.wordLinkId, self.state.word, self.props.session.WikiSessionId)
            .then(res=>{
                return res;
            });
        self.props.linkClicked(res);
    };
    render(){
        let self = this;
        return (
            <Card square={true}  align={"center"} variant={"outlined"}>
                <CardContent >
                    {this.state.word}
                </CardContent>
                <CardActions>
                    <Button variant="outlined" color="primary" onClick={val=>self.performClick(self)}>
                        Choose
                    </Button>
                </CardActions>
            </Card>
        );
    }
}
const mapStateToProps = state =>{
  return state;
};
export default connect(mapStateToProps, {linkClicked})(WikiLinkButton);