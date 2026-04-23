// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
if (window.location.pathname === "/") {
    const academias = document.querySelectorAll(".academia");
    academias.forEach((academia) => {
        academia.addEventListener("click", () => {
            const poblacion = academia.querySelector("span").textContent.trim();
            if (poblacion) {
                poblacion.style.display = "none";
            }
        });
    });
}