import React, { useState } from "react";
import "../App.css";
import { updateClub, deleteClub } from "../services/api";

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

    try {
      const updatedClub = await updateClub(club.id, {
        name: editedClub.clubName,
        numberoftrophies: editedClub.trophyCount,
      });
      onUpdateClub(updatedClub);
      setIsEditing(false);
    } catch (error) {
      console.error("Error updating club:", error);
    }
  }

  async function handleDelete() {
    try {
      await deleteClub(club.id);
      onDeleteClub(club.id);
    } catch (error) {
      console.error("Error deleting club:", error);
    }
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
