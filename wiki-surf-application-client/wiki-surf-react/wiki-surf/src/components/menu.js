import React, {Component} from 'react';
import {AppBar, Toolbar, Typography, Divider, Grid }from '@material-ui/core';

import {connect} from "react-redux";
import {
    getStartingWord,
    getEndingWord,
    getPlayerName,
    getTotalClicks,
    getWikiSessionId
} from "../redux/selectors";




class WikiMenu extends Component{

    constructor(props){

        super(props);

        this.state = {

        };

    }

    render() {
        let self = this;
        return (
            <div >
                <AppBar position="fixed">
                    <Toolbar>
                        <Grid container direction = "row" spacing={3}>
                            <Grid item xs={3}>
                                <Typography variant="h5" >
                                    Starting Word:
                                </Typography>
                                <Typography variant = "h6">
                                    {this.props.startingWord}
                                </Typography>
                            </Grid>

                            <Grid item xs={3}>
                                <Typography variant="h5" >
                                    Ending Word:
                                </Typography>
                                <Typography variant = "h6">
                                    {this.props.endingWord}
                                </Typography>
                            </Grid>

                            <Grid item xs={4}>
                                <Typography variant="h5">Session ID: </Typography>
                                <Typography variant = "h6">
                                    {this.props.sessionId}
                                </Typography>
                            </Grid>

                            <Grid item xs={2}>
                                <Typography variant="h5">Total Clicks: </Typography>
                                <Typography variant = "h6">
                                    {this.props.totalClicks}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Toolbar>
                </AppBar>
            </div>
        );
    }
}

const mapStateToProps = state =>{

  return {
      sessionId: getWikiSessionId(state),
      startingWord: getStartingWord(state),
      endingWord: getEndingWord(state),
      totalClicks: getTotalClicks(state),
      playerName: getPlayerName(state)
  };
};
export default connect(mapStateToProps)(WikiMenu);