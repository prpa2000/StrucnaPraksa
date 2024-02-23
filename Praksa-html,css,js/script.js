window.addEventListener("DOMContentLoaded", () => {
  var storedClubs = localStorage.getItem("clubs");
  if (storedClubs) {
    clubs = JSON.parse(storedClubs);
    displayClubs();
  }
});

function addClub(e) {
  e.preventDefault();
  const clubId = document.getElementById("clubId").value;
  const clubName = document.getElementById("clubName").value;
  const trophyCount = document.getElementById("trophyCount").value;
  const year = document.getElementById("year").value;
  if (clubs.some((club) => club.name === clubName)) {
    alert("Club with that name already exists!");
    document.getElementById("clubForm").reset();
    return;
  }
  const club = {
    index: clubId,
    name: clubName,
    trophies: trophyCount,
    year: year,
  };
  clubs.push(club);
  localStorage.setItem("clubs", JSON.stringify(clubs));
  displayClubs();

  document.getElementById("clubForm").reset();
}

function displayClubs() {
  const clubList = document.getElementById("clubList");
  clubList.innerHTML = "";
  clubs.forEach(function (club) {
    const row = document.createElement("tr");
    row.innerHTML = `
                <td>${club.name}</td>
                <td>${club.trophies}</td>
                <td>${club.year}</td>
                <td>
                    <a class="editclub" href="editclub.html?clubId=${club.index}&clubName=${club.name}&trophyCount=${club.trophies}&year=${club.year}">Uredi</a>
                    <button class="deleteclub" onclick="deleteClub('${club.name}')">Izbriši</button>
                </td>
            `;
    clubList.appendChild(row);
  });
}

function deleteClub(clubName) {
  clubs = clubs.filter((club) => club.name !== clubName);
  localStorage.setItem("clubs", JSON.stringify(clubs));
  displayClubs();
}

function updateClub() {
  const urlParams = new URLSearchParams(window.location.search);
  const clubId = urlParams.get("clubId");
  const clubName = document.getElementById("clubName").value;
  const trophyCount = document.getElementById("trophyCount").value;
  const year = document.getElementById("year").value;

  const index = clubs.findIndex((club) => club.index === clubId);
  if (index !== -1) {
    clubs[index].name = clubName;
    clubs[index].trophies = trophyCount;
    clubs[index].year = year;

    localStorage.setItem("clubs", JSON.stringify(clubs));
    document.getElementById("clubForm").reset();
    displayClubs();
  }
}

window.onload = function () {
  const updateForm = document.getElementById("clubForm");
  updateForm.addEventListener("submit", addClub);
  updateForm.addEventListener("submit", updateClub);
};
