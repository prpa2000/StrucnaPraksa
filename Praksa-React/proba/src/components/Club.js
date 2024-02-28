import React, { useState } from "react";
import "../App.css";
import axios from "axios";
function Club({ club, onDeleteClub, onUpdateClub }) {
  const [isEditing, setIsEditing] = useState(false);
  const [editedClub, setEditedClub] = useState({
    clubId: club.clubId || "",
    clubName: club.clubName || "",
    trophyCount: club.trophyCount || "",
  });

  function handleEdit() {
    setIsEditing(true);
  }

  function handleChange(e) {
    const { name, value } = e.target;
    setEditedClub({ ...editedClub, [name]: value });
  }

  async function handleUpdate() {
    if (!editedClub.clubName || !editedClub.trophyCount) {
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
    const editedFootballClub = {
      id: editedClub.clubId,
      name: editedClub.clubName,
      numberoftrophies: editedClub.trophyCount,
    };
    await axios
      .put(
        `https://localhost:44392/api/FootballClub/${club.id}`,
        editedFootballClub
      )
      .then((response) => {
        const updatedClub = response.data;
        console.log(club.id);
        console.log(updatedClub);
        editedClub.clubId = club.id;
        console.log(editedClub);
        onUpdateClub(editedClub);
        setIsEditing(false);
      })
      .catch((error) => {
        console.error("Error updating club:", error);
      });
  }

  function handleDelete() {
    const clubsFromStorage = JSON.parse(localStorage.getItem("clubs")) || [];
    const updatedClubs = clubsFromStorage.filter((c) => c.id !== club.id);
    localStorage.setItem("clubs", JSON.stringify(updatedClubs));
    onDeleteClub(club.id);
  }

  return (
    <tr>
      <td>{club.id}</td>
      <td>
        {isEditing ? (
          <input
            type="text"
            name="clubName"
            value={editedClub.clubName}
            onChange={handleChange}
          />
        ) : (
          club.name
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
          club.numberOfTrophies
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
