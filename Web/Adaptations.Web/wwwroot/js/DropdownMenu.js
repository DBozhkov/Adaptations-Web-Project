document.addEventListener("DOMContentLoaded", function () {
    var menuButton = document.getElementById("menuButton");
    var menuContent = document.getElementById("menuContent");

    menuButton.addEventListener("click", function (e) {
        e.preventDefault();
        menuContent.style.display = (menuContent.style.display === "none") ? "block" : "none";
    });

    var menuText = document.querySelector("#menuButton .ipc-responsive-button__text");
    var menuLabel = document.getElementById("header");

    menuLabel.style.display = "flex";
    menuLabel.style.alignItems = "center";
});

document.addEventListener("DOMContentLoaded", function () {
    var seeBioElement = document.getElementById("seeBio");
    var fullBioElement = document.getElementById("fullBio");
    var shortBioElement = document.getElementById("shortBio");

    seeBioElement.addEventListener('click', (e) => {
        e.preventDefault();

        if (shortBioElement.style.display === "block") {
            shortBioElement.style.display = "none";
            fullBioElement.style.display = "block";
            seeBioElement.textContent = "Hide full bio";
        } else {
            seeBioElement.textContent = "See full bio";
            shortBioElement.style.display = "block";
            fullBioElement.style.display = "none";
        }
    })
});

document.addEventListener("DOMContentLoaded", function () {
    var filmographyElement = document.querySelector(".topbar-filter");
    var seeFilmsElement = document.getElementById("seeFilmography");

    seeFilmsElement.addEventListener('click', (e) => {
        e.preventDefault();

        if (filmographyElement.style.display === "none") {
            console.log("Show Filmography");
            filmographyElement.style.display = "block";
            seeFilmsElement.textContent = "Hide all filmography";
        } else {
            console.log("Hide Filmography");
            seeFilmsElement.textContent = "See full filmography";
            filmographyElement.style.display = "none";
        }
    })
});

document.addEventListener("DOMContentLoaded", function () {
    var searchButton = document.getElementById("searchButton");
    var searchForm = document.querySelector(".search-form");

    searchButton.addEventListener('click', (e) => {
        e.preventDefault();

        var searchInput = document.getElementById("searchInput");

        var inputValue = searchInput.value.trim();

        if (inputValue) {
            searchForm.action = '/Search/GetResult?searchInput=' + encodeURIComponent(inputValue);

            searchForm.submit();
        }
    })
});