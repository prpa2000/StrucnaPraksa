import React, { useState } from "react";
import "../App.css";
function Club({ club, onDeleteClub, onUpdateClub }) {
  const [isEditing, setIsEditing] = useState(false);
  const [editedClub, setEditedClub] = useState({
    clubId: club.clubId,
    clubName: club.clubName,
    trophyCount: club.trophyCount,
    year: club.year,
  });

  function handleEdit() {
    setIsEditing(true);
  }

  function handleChange(e) {
    const { name, value } = e.target;
    setEditedClub({ ...editedClub, [name]: value });
  }

  function handleUpdate() {
    if (!editedClub.clubName || !editedClub.trophyCount || !editedClub.year) {
      alert("Ne mogu biti prazna polja prilikom ažuriranja!");
      return;
    }
    if (editedClub.clubName.length < 3) {
      alert("Minimalno 3 znaka!");
      return;
    }
    if (editedClub.trophyCount < 0) {
      alert("Ne može biti negativan broj!");
      return;
    }
    if (editedClub.year < 0) {
      alert("Ne može biti negativan broj!");
      return;
    }
    onUpdateClub(editedClub);
    setIsEditing(false);

    const clubsFromStorage = JSON.parse(localStorage.getItem("clubs")) || [];
    const updatedClubs = clubsFromStorage.map((club) =>
      club.clubId === editedClub.clubId ? editedClub : club
    );
    localStorage.setItem("clubs", JSON.stringify(updatedClubs));
  }

  function handleDelete() {
    const clubsFromStorage = JSON.parse(localStorage.getItem("clubs")) || [];
    const updatedClubs = clubsFromStorage.filter(
      (c) => c.clubId !== club.clubId
    );
    localStorage.setItem("clubs", JSON.stringify(updatedClubs));
    onDeleteClub(club.clubId);
  }

  return (
    <tr>
      <td>{club.clubId}</td>
      <td>
        {isEditing ? (
          <input
            type="text"
            name="clubName"
            value={editedClub.clubName}
            onChange={handleChange}
          />
        ) : (
          club.clubName
        )}
      </td>
      <td>
        {isEditing ? (
          <input
            type="number"
            name="trophyCount"
            value={editedClub.trophyCount}
            onChange={handleChange}
          />
        ) : (
          club.trophyCount
        )}
      </td>
      <td>
        {isEditing ? (
          <input
            type="number"
            name="year"
            value={editedClub.year}
            onChange={handleChange}
          />
        ) : (
          club.year
        )}
      </td>
      <td>
        {isEditing ? (
          <button onClick={handleUpdate} className="savebutton">
            Spremi
          </button>
        ) : (
          <>
            <button onClick={handleEdit} className="editbutton">
              Uredi
            </button>
            <button onClick={handleDelete} className="deletebutton">
              Izbriši
            </button>
          </>
        )}
      </td>
    </tr>
  );
}

export default Club;
