import React, { useState } from "react";
import "../App.css";
function ClubForm({ onAddClub }) {
  const [clubId, setClubId] = useState("");
  const [clubName, setClubName] = useState("");
  const [trophyCount, setTrophyCount] = useState("");
  const [year, setYear] = useState("");
  const [formError, setFormError] = useState(null);
  function handleChange(e) {
    const { name, value } = e.target;
    switch (name) {
      case "clubId":
        setClubId(value);
        break;
      case "clubName":
        setClubName(value);
        break;
      case "trophyCount":
        setTrophyCount(value);
        break;
      case "year":
        setYear(value);
        break;
      default:
        break;
    }
  }
  function handleSubmit(e) {
    e.preventDefault();
    if (!clubId || !clubName || !trophyCount || !year) {
      setFormError("Popunite sva polja.");
      return;
    }
    if (clubId < 0) {
      setFormError("Id kluba ne može biti negativan broj.");
      return;
    }
    if (clubName.length < 3) {
      setFormError("Ime kluba mora sadržavati minimalno 3 znaka.");
      return;
    }
    if (trophyCount < 0) {
      setFormError("Broj trofeja ne može biti negativan broj.");
      return;
    }
    if (year < 0) {
      setFormError("Godina osnutka ne može biti negativan broj.");
      return;
    }

    const clubsFromStorage = JSON.parse(localStorage.getItem("clubs")) || [];
    const existingClub = clubsFromStorage.find(
      (club) => club.clubId === clubId
    );
    if (existingClub) {
      setFormError("Klub s istim id-em već postoji.");
      return;
    }

    const newClub = {
      clubId,
      clubName,
      trophyCount,
      year,
    };
    clubsFromStorage.push(newClub);
    localStorage.setItem("clubs", JSON.stringify(clubsFromStorage));
    onAddClub(newClub);
    console.log("Podaci o novom klubu:", newClub);
    setFormError("");
    setClubId("");
    setClubName("");
    setTrophyCount("");
    setYear("");

    console.log(clubId);
  }

  return (
    <form className="clubForm">
      <div>
        <label htmlFor="clubId">Id:</label>
        <input
          type="number"
          id="clubId"
          name="clubId"
          value={clubId}
          onChange={handleChange}
          required
        />
      </div>
      <div className="clubname">
        <label htmlFor="clubName">Ime kluba:</label>
        <input
          type="text"
          id="clubName"
          name="clubName"
          value={clubName}
          onChange={handleChange}
          required
        />
      </div>
      <div className="clubtrophies">
        <label htmlFor="trophyCount">Broj trofeja:</label>
        <input
          type="number"
          id="trophyCount"
          value={trophyCount}
          name="trophyCount"
          onChange={handleChange}
          required
        />
      </div>
      <div className="clubyear">
        <label htmlFor="year">Godina osnutka:</label>
        <input
          type="number"
          id="year"
          name="year"
          value={year}
          onChange={handleChange}
          required
        />
      </div>
      {formError && <p style={{ color: "red" }}>{formError}</p>}
      <div>
        <button type="submit" onClick={handleSubmit}>
          Dodaj klub
        </button>
      </div>
    </form>
  );
}

export default ClubForm;
