import "./App.css";

import ClubForm from "./components/ClubForm";
import ClubList from "./components/ClubList";

import { useState, useEffect } from "react";
import { fetchClubs, addClub } from "./services/api";
import { createBrowserRouter, Outlet, RouterProvider } from "react-router-dom";
import Home from "./pages/Home";
import Navbar from "./components/Navbar";
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
    fetchData();
  }, [filter]);

  async function fetchData() {
    try {
      const data = await fetchClubs(filter);
      setClubs(data);
    } catch (error) {
      console.error("Error fetching clubs:", error);
    }
  }
  async function handleAddClub(newClub) {
    try {
      await addClub(newClub);
      fetchData();
    } catch (error) {
      console.error("Error adding club:", error);
    }
  }

  const Layout = () => {
    return (
      <>
        <Navbar></Navbar>
        <Outlet></Outlet>
      </>
    );
  };
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
        { path: "/", element: <Home /> },
        { path: "/add-clubs", element: <ClubForm onAddClub={handleAddClub} /> },
        { path: "/clubs", element: <ClubList clubs={clubs} /> },
      ],
    },
  ]);
  return (
    <div className="app">
      <div className="container">
        <RouterProvider router={router} />
      </div>
    </div>
  );
}

export default App;
