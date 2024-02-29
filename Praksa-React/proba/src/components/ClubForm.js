import React, { useState } from "react";
import "../App.css";
import { addClub } from "../services/api";
import { useNavigate } from "react-router-dom";

function ClubForm({ onAddClub }) {
  const [formData, setFormData] = useState({
    clubId: "",
    clubName: "",
    trophyCount: "",
  });
  const [formError, setFormError] = useState(null);
  const navigate = useNavigate();
  function handleChange(e) {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  }

  async function handleSubmit(e) {
    e.preventDefault();

    if (!formData.clubId || !formData.clubName || !formData.trophyCount) {
      setFormError("Popunite sva polja.");
      return;
    }
    if (formData.clubId < 0) {
      setFormError("Id kluba ne može biti negativan broj.");
      return;
    }
    if (formData.clubName.length < 3) {
      setFormError("Ime kluba mora sadržavati minimalno 3 znaka.");
      return;
    }
    if (formData.trophyCount < 0) {
      setFormError("Broj trofeja ne može biti negativan broj.");
      return;
    }

    try {
      const newFootballClub = {
        id: formData.clubId,
        name: formData.clubName,
        numberoftrophies: formData.trophyCount,
      };
      const addedClub = await addClub(newFootballClub);
      onAddClub(addedClub);
      setFormError("");
      setFormData({
        clubId: "",
        clubName: "",
        trophyCount: "",
      });

      navigate("/clubs");
    } catch (error) {
      console.error("Error adding new club:", error);
    }
  }

  return (
    <form className="clubForm" onSubmit={handleSubmit}>
      <div>
        <label htmlFor="clubId">Id:</label>
        <input
          type="number"
          id="clubId"
          name="clubId"
          value={formData.clubId}
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
          value={formData.clubName}
          onChange={handleChange}
          required
        />
      </div>
      <div className="clubtrophies">
        <label htmlFor="trophyCount">Broj trofeja:</label>
        <input
          type="number"
          id="trophyCount"
          value={formData.trophyCount}
          name="trophyCount"
          onChange={handleChange}
          required
        />
      </div>

      {formError && <p style={{ color: "red" }}>{formError}</p>}
      <div>
        <button type="submit">Dodaj klub</button>
      </div>
    </form>
  );
}

export default ClubForm;
