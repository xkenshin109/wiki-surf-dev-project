import { combineReducers } from "redux";
import sessionTodos from './sessionTodo';
import gameEngine from './gameEngine';
export default combineReducers({session: sessionTodos, wikiEngine: gameEngine});
