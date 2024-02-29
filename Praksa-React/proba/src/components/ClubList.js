import React, { useState, useEffect } from "react";
import Club from "./Club";
import FilterForm from "./FilterForm";
import { fetchClubs, deleteClub, updateClub } from "../services/api";

function ClubList() {
  const [clubs, setClubs] = useState([]);
  const [filter, setFilter] = useState({
    pagenumber: 1,
    pagesize: 10,
    sortby: "",
    sortorder: "",
    name: "",
    numberoftrophies: "",
  });

  useEffect(() => {
    fetchData(filter);
  }, [filter]);

  async function fetchData(filter) {
    try {
      const data = await fetchClubs(filter);
      setClubs(data);
    } catch (error) {
      console.error("Error fetching clubs:", error);
    }
  }

  function handleFilterSubmit(filter) {
    setFilter(filter);
  }

  async function handleDeleteClub(id) {
    try {
      await deleteClub(id);
    } catch (error) {
      console.error("Error deleting club:", error);
    }
    fetchData(filter);
  }

  async function handleUpdateClub(updatedClub) {
    try {
      await updateClub(updatedClub.clubId, updatedClub);
    } catch (error) {
      console.error("Error updating club:", error);
    }
    fetchData(filter);
  }

  return (
    <div className="clubListContainer">
      <FilterForm onSubmit={handleFilterSubmit} />
      {clubs.length > 0 ? (
        <table className="clubTable">
          <thead>
            <tr>
              <th>Id</th>
              <th>Ime kluba</th>
              <th>Broj trofeja</th>
              <th>Akcije</th>
            </tr>
          </thead>
          <tbody>
            {clubs.map((club, id) => (
              <Club
                key={id}
                club={club}
                onDeleteClub={handleDeleteClub}
                onUpdateClub={handleUpdateClub}
              />
            ))}
          </tbody>
        </table>
      ) : (
        <div className="noClubsMessage">No clubs available</div>
      )}
    </div>
  );
}

export default ClubList;
