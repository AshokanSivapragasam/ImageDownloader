import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link, NavLink } from "react-router-dom";
import Board from './Board';
import logo from './logo.svg';
import './App.css';
import LayoutHeader from './components/layout-header/LayoutHeader';
import LayoutTwoLayerFlyMenu from './components/layout-twolayer-fly-menu/LayoutTwoLayerFlyMenu';

class App extends Component {
  render() {
    return (
      <section>
        <LayoutHeader />
        <LayoutTwoLayerFlyMenu />
      </section>
    );
  }

  renderNameTag(name) {
    return name;
  }
}

export default App;
