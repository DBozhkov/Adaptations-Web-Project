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