import React from "react";
import "../App.css";
import image from "../images/s960_Football_gov.uk.jpg";
function Home() {
  return (
    <div className="home-container">
      <h1>
        Dobrodošli na našu aplikaciju za upravljanje nogometnim klubovima!
      </h1>
      <p>
        Ovdje možete dodavati nove klubove, pregledavati postojeće klubove,
        primjenjivati filtere za pretraživanje i još mnogo toga.
      </p>
      <p>
        Počnite s dodavanjem novih klubova ili pregledom postojećih klubova
        klikom na odgovarajuće poveznice u navigaciji iznad.
      </p>
      <img
        src={image}
        alt="Nogometna slika"
        className="football-image"
      />
    </div>
  );
}

export default Home;
