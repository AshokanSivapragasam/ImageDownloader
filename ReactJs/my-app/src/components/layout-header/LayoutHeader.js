import React, { Component } from 'react';
import './LayoutHeader.css';
import Button from '@material-ui/core/Button';

class LayoutHeader extends Component {
    render() {
        return <><section className="quicklinks">
            <span className="quicklinks-container">
            <span className="quicklinks-lhs">
                <span><a href={this.props.tryIn3d}>Try in 3D</a></span>
                <span><a href={this.props.tryIn3d}>Home Eye Test</a></span>
                <span><a href={this.props.tryIn3d}>Franchise</a></span>
                <span><a href={this.props.tryIn3d}>Lenskart At Office</a></span>
                <span><a href={this.props.tryIn3d}>Blog</a></span>
                <span><a href={this.props.tryIn3d}>Refer &amp; Earn</a></span>
            </span>
            <span className="quicklinks-rhs">
                <span><a href={this.props.tryIn3d}>Locate store: 1800 111 111</a></span>
                <span><a href={this.props.tryIn3d}>Support: (0)99998 99998</a></span>
                <span><a href={this.props.tryIn3d}>www.johnjacobs.com</a></span>
            </span>
            </span>
        </section>
        <section className="header-container">
            <section className="mat-toolbar">
                <span className="left-hand-side">
                    <img src="//static.lenskart.com/media/desktop/img/menu/logo.png" width="186.73" height="25.16" alt="lenskart" title="lenskart"/>
                    <span>
                        <input type="search" className="searchBox" placeholder="ElementId:12345, Blu Lens, Polariods,.."/>
                    </span>
                </span>
                <span className="right-hand-side">
                    <span>
                        <Button onClick={this.register} variant="text" color="inherit">Register</Button>
                        <Button onClick={this.login} variant="text" color="inherit">Login</Button>
                    </span>
                    <span>
                        <Button onClick={this.acquireTracker} variant="text" color="inherit">Tracker</Button>
                        <Button onClick={this.getWishList} variant="text" color="inherit">Wishlist</Button>
                        <Button onClick={this.seeWhatIsInCart} variant="text" color="inherit">Cart</Button>
                    </span>
                </span>
            </section>
        </section></>;
    }

    register() {
    }

    login() {
    }

    logout() {
    }

    acquireTracker() {
        console.log('acquireTracker');
    }

    getWishList() {
    }

    seeWhatIsInCart() {
    }
}

export default LayoutHeader;