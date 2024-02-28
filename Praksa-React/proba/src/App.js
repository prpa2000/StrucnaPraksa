import "./App.css";
import axios from "axios";
import ClubForm from "./components/ClubForm";
import ClubList from "./components/ClubList";
import FilterForm from "./components/FilterForm";
import { useState, useEffect } from "react";

function App() {
  const [clubs, setClubs] = useState([]);
  const [filter, setFilter] = useState({
    pagenumber: 1,
    pagesize: 10,
    sortby: "",
    sortorder: "",
    id: null,
    name: "",
    numberoftrophies: null,
  });
  useEffect(() => {
    const fetchClubs = async () => {
      try {
        const response = await axios.get(
          "https://localhost:44392/api/FootballClub"
        );
        setClubs(response.data);
      } catch (error) {
        console.error("Error fetching clubs:", error);
      }
    };

    fetchClubs();
  }, []);

  async function fetchClubs() {
    try {
      const response = await axios.get(
        "https://localhost:44392/api/FootballClub",
        { params: filter }
      );
      setClubs(response.data);
    } catch (error) {
      console.error("Error fetching clubs:", error);
    }
  }
  useEffect(() => {
    fetchClubs();
  }, [filter]);

  async function addClub(newClub) {
    setClubs([...clubs, newClub]);
    const response = await axios.get(
      "https://localhost:44392/api/FootballClub"
    );
    setClubs(response.data);
  }

  async function deleteClub(id) {
    try {
      await axios.delete(`https://localhost:44392/api/FootballClub/${id}`);
      setClubs(clubs.filter((club) => club.clubId !== id));
    } catch (error) {
      console.error("Error deleting club:", error);
    }
    const response = await axios.get(
      "https://localhost:44392/api/FootballClub"
    );
    setClubs(response.data);
  }

  async function updateClub(updatedClub) {
    setClubs(
      clubs.map((club) =>
        club.clubId === updatedClub.clubId ? updatedClub : club
      )
    );
    const response = await axios.get(
      "https://localhost:44392/api/FootballClub"
    );
    setClubs(response.data);
  }

  async function getFilteredClubs(filters) {
    console.log(filters);
    try {
      let queryParams = new URLSearchParams();
      for (const key in filters) {
        if (filters[key]) {
          queryParams.append(key, filters[key]);
        }
      }

      const url = `https://localhost:44392/api/FootballClub?${queryParams.toString()}`;
      const response = await axios.get(url);
      console.log(url);
      console.log(response.data);
      setClubs(response.data);
    } catch (error) {
      console.error("Error fetching filtered clubs:", error);
    }
  }

  function handleFilterSubmit(filters) {
    console.log("Filters submitted:", filters);
    getFilteredClubs(filters);
  }

  return (
    <div className="App">
      <header className="App-header">
        <h2>DODAJ KLUB</h2>
        <ClubForm onAddClub={addClub} />
        <h2>POPIS KLUBOVA</h2>
        <FilterForm onSubmit={handleFilterSubmit} />
        <ClubList
          clubs={clubs}
          onDeleteClub={deleteClub}
          onUpdateClub={updateClub}
        />
      </header>
    </div>
  );
}

export default App;
