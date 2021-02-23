import { NEW_SESSION } from "../actionTypes";

const initialState = {
    WikiSessionId: 'dcf3f18a-cd74-eb11-91d6-e0d55ee1c539',
    processing: false
};

export default function(state = initialState, action){
    switch (action.type) {
        case NEW_SESSION:
        {
            const {WikiSessionId} = action.payload;
            return {
                ...state,
                WikiSessionId
            }
        }
        default:
            return state;
    }
}