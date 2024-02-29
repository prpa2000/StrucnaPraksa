import React from "react";
import { Link } from "react-router-dom";
import "../styles/Navbar.css";
function Navbar() {
  return (
    <nav>
      <ul>
        <li>
          <Link to="/">Početna</Link>
        </li>
        <li>
          <Link to="/clubs">Popis klubova</Link>
        </li>
        <li>
          <Link to="/add-clubs">Dodaj klub</Link>
        </li>
      </ul>
    </nav>
  );
}

export default Navbar;
