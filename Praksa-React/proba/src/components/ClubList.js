import React from "react";
import Club from "./Club";
import "../App.css";
function ClubList({ clubs, onDeleteClub, onUpdateClub }) {
  return (
    <table className="clubTable">
      <thead>
        <tr>
          <th>Id</th>
          <th>Ime kluba</th>
          <th>Broj trofeja</th>
          <th>Godina osnutka</th>
          <th>Akcije</th>
        </tr>
      </thead>
      <tbody>
        {clubs.map((club) => (
          <Club
            key={club.clubId}
            club={club}
            onDeleteClub={onDeleteClub}
            onUpdateClub={onUpdateClub}
          />
        ))}
      </tbody>
    </table>
  );
}

export default ClubList;
