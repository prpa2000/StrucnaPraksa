import React, { useState } from "react";
import "../App.css";

function FilterForm({ onSubmit }) {
  const [filters, setFilters] = useState({
    pagenumber: 1,
    pagesize: 10,
    sortby: "",
    sortorder: "",
    name: "",
    numberoftrophies: "",
  });
  const [showFilters, setShowFilters] = useState(false);

  function handleInputChange(e) {
    const { name, value } = e.target;
    setFilters((prevFilters) => ({
      ...prevFilters,
      [name]: value,
    }));
  }

  function handleSubmit(e) {
    e.preventDefault();
    onSubmit(filters);
  }

  function toggleFilters() {
    setShowFilters(!showFilters);
  }

  return (
    <div className="form-container">
      <button onClick={toggleFilters} className="filterbutton">
        Filteri
      </button>
      {showFilters && (
        <form onSubmit={handleSubmit}>
          <label>
            Broj stranice:
            <input
              type="number"
              name="pagenumber"
              value={filters.pagenumber}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Broj klubova po stranici:
            <input
              type="number"
              name="pagesize"
              value={filters.pagesize}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Sortiraj po:
            <select
              name="sortby"
              value={filters.sortby}
              onChange={handleInputChange}
            >
              <option value="">- </option>
              <option value="Name">Ime kluba</option>
              <option value="NumberOfTrophies">Broj trofeja</option>
            </select>
          </label>
          <label>
            Redoslijed sortiranja:
            <select
              name="sortorder"
              value={filters.sortorder}
              onChange={handleInputChange}
            >
              <option value=""> -</option>
              <option value="asc">Rastući</option>
              <option value="desc">Padajući</option>
            </select>
          </label>
          <label>
            Ime kluba:
            <input
              type="text"
              name="name"
              value={filters.name}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Broj trofeja:
            <input
              type="number"
              name="numberoftrophies"
              value={filters.numberoftrophies}
              onChange={handleInputChange}
            />
          </label>
          <button type="submit">Primijeni</button>
        </form>
      )}
    </div>
  );
}

export default FilterForm;
