import React from 'react';
import ReactDOM from 'react-dom';
import {Provider } from 'react-redux'
import store from './redux/store';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { createMuiTheme, responsiveFontSizes, ThemeProvider } from '@material-ui/core/styles';
import purple from "@material-ui/core/colors/blue";
import green from "@material-ui/core/colors/green";
let theme = createMuiTheme({
    palette: {
        primary: {
            main: purple[500],
        },
        secondary: {
            main: green[500],
        },
    }
});
theme = responsiveFontSizes(theme);
ReactDOM.render(
      <React.StrictMode>
          <Provider store={store}>
              <ThemeProvider theme={theme}>
                  <App />
              </ThemeProvider>
          </Provider>
      </React.StrictMode>,
      document.getElementById('root')

);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
