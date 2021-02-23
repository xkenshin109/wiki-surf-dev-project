import {GAME_START, GAME_CONTENT, NAVIGATE, GAME_LINK_CLICK} from "../actionTypes";
import {SCREEN_MAIN} from "../../screen_constants";

const initialState = {
    screen: SCREEN_MAIN,
    gameStarted: false,
    wikiContent:{
        session:{

        }
    }
};
export default function(state = initialState, action){
    switch (action.type) {
        case GAME_START:
        {
            return{
                ...state,
                gameStarted: action.payload
            }
        }
        case GAME_CONTENT:
        {
            const{startingWord, endingWord, startingPage, pageLinks} = action.payload;
            return{
                ...state,
                wikiContent:{
                    startingWord,
                    endingWord,
                    pageContent: startingPage,
                    pageLinks
                }
            }
        }
        case NAVIGATE:
        {
            return{
                ...state,
                screen: action.payload
            }
        }
        case GAME_LINK_CLICK:
            const {session, content} = action.payload;
            return{
                ...state,
                ...session,
                content
            };
        default:
            return state;
    }
}