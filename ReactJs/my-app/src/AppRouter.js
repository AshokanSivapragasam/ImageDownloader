import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';

const RouterApp = () => (
    <Router>
      <>
        <Header />
  
        <Route exact path="/" component={Home} />
        <Route path="/about" component={About} />
        <Route path="/topic/:id" component={Topic} />
        <Route path="/topics" component={Topics} />
      </>
    </Router>
  );
  
  const Home = () => <h2>Home</h2>;
  const About = () => <h2>About</h2>;
  const Topic = ({ match }) => <h3>Requested Param: {match.params.id}</h3>;
  const Topics = ({ match }) => (
    <div>
      <h2>Topics</h2>
  
      <ul>
        <li>
          <Link to={`${match.url}/components`}>Components</Link>
        </li>
        <li>
          <Link to={`${match.url}/props-v-state`}>Props v. State</Link>
        </li>
      </ul>
  
      <Route path={`${match.path}/:id`} component={Topic} />
      <Route
        exact
        path={match.path}
        render={() => <h3>Please select a topic.</h3>}
      />
    </div>
  );
  const Header = () => (
    <ul>
      <li>
        <Link to="/">Home</Link>
      </li>
      <li>
        <Link to="/about">About</Link>
      </li>
      <li>
        <Link to="/topics">Topics</Link>
      </li>
    </ul>
  );

export default RouterApp;