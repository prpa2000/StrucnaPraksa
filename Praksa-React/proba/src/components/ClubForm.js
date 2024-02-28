import React, { useState } from "react";
import "../App.css";
import axios from "axios";
function ClubForm({ onAddClub }) {
  const [clubId, setClubId] = useState("");
  const [clubName, setClubName] = useState("");
  const [trophyCount, setTrophyCount] = useState("");
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

      default:
        break;
    }
  }
  async function handleSubmit(e) {
    e.preventDefault();

    if (!clubId || !clubName || !trophyCount) {
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

    const existingClub = await checkExistingClub(clubId);
    if (existingClub) {
      setFormError("Klub s unesenim ID-om već postoji.");
      return;
    }
    const newFootballClub = {
      id: clubId,
      name: clubName,
      numberoftrophies: trophyCount,
    };
    axios
      .post(`https://localhost:44392/api/FootballClub`, newFootballClub)
      .then((response) => {
        const newClub = response.data;
        console.log(newClub);
        onAddClub(newClub);
        setFormError("");
        setClubId("");
        setClubName("");
        setTrophyCount("");
      })
      .catch((error) => {
        console.error("Error adding new club:", error);
      });
  }
  async function checkExistingClub(id) {
    try {
      const response = await axios.get(
        `https://localhost:44392/api/FootballClub/${id}`
      );
      return response.data ? true : false;
    } catch (error) {
      return false;
    }
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