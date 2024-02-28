import React, { useState } from "react";
import "../App.css";
function FilterForm({ onSubmit }) {
  const [filters, setFilters] = useState({
    pagenumber: 1,
    pagesize: 10,
    sortby: "",
    sortorder: "",
    id: "",
    name: "",
    numberoftrophies: "",
  });
  const [showFilters, setShowFilters] = useState(false);
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFilters((prevFilters) => ({
      ...prevFilters,
      [name]: value,
    }));
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(filters);
  };
  const toggleFilters = () => {
    setShowFilters(!showFilters);
  };
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
            Sortiraj po (Name, NumberOfTrophies):
            <input
              type="text"
              name="sortby"
              value={filters.sortby}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Sort Order:
            <input
              type="text"
              name="sortorder"
              value={filters.sortorder}
              onChange={handleInputChange}
            />
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

          <button type="submit">Apply Filters</button>
        </form>
      )}
    </div>
  );
}

export default FilterForm;
