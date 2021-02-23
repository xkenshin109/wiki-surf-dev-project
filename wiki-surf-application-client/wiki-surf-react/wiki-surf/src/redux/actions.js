import {NEW_SESSION, GAME_START, GAME_CONTENT, NAVIGATE, GAME_LINK_CLICK} from "./actionTypes";
import wikiApi from '../utils/wiki-api';
export const newSession = content =>{
    return ({
        type: NEW_SESSION,
        payload:{
            ...content
        }
    });
};

export const gameStart = content =>({
   type: GAME_START,
    payload: content === true? content: false
});

export const gameContent = content =>({
   type: GAME_CONTENT,
   payload: content
});

export const navigate = screen => ({
   type: NAVIGATE,
   payload: screen
});

export const linkClicked = content =>({
   type: GAME_LINK_CLICK,
   payload: content
});