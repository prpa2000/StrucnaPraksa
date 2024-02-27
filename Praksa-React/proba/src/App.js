import "./App.css";

import ClubForm from "./components/ClubForm";
import ClubList from "./components/ClubList";
import { useState } from "react";
function App() {
  const [clubs, setClubs] = useState([]);

  function addClub(newClub) {
    setClubs([...clubs, newClub]);
  }

  function deleteClub(id) {
    setClubs(clubs.filter((club) => club.clubId !== id));
  }

  function updateClub(updatedClub) {
    setClubs(
      clubs.map((club) =>
        club.clubId === updatedClub.clubId ? updatedClub : club
      )
    );
  }

  return (
    <div className="App">
      <header className="App-header">
        <h2>DODAJ KLUB</h2>
        <ClubForm onAddClub={addClub}></ClubForm>
        <h2>POPIS KLUBOVA</h2>
        <ClubList
          clubs={clubs}
          onDeleteClub={deleteClub}
          onUpdateClub={updateClub}
        ></ClubList>
      </header>
    </div>
  );
}

export default App;
