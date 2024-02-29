import axios from "axios";

const API_BASE_URL = "https://localhost:44392/api";

const api = axios.create({
  baseURL: API_BASE_URL,
});

export async function fetchClubs(filter) {
  try {
    let url = "/FootballClub";
    const params = Object.entries(filter)
      .filter(
        ([key, value]) => value !== null && value !== undefined && value !== ""
      )
      .map(([key, value]) => `${key}=${encodeURIComponent(value)}`)
      .join("&");

    if (params) {
      url += `?${params}`;
    }

    const response = await api.get(url);
    return response.data;
  } catch (error) {
    console.error("Error fetching clubs:", error);
    throw error;
  }
}

export async function addClub(newClub) {
  try {
    const response = await api.post("/FootballClub", newClub);
    const addedClub = response.data;
    return addedClub;
  } catch (error) {
    console.error("Error adding club:", error);
    throw error;
  }
}

export async function deleteClub(id) {
  try {
    await api.delete(`/FootballClub/${id}`);
    return id;
  } catch (error) {
    console.error("Error deleting club:", error);
    throw error;
  }
}

export async function updateClub(id, updatedClub) {
  try {
    const response = await api.put(`/FootballClub/${id}`, updatedClub);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error("Error updating club:", error);
    throw error;
  }
}
