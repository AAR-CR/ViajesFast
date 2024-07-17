

document.addEventListener("DOMContentLoaded", function () {
    const table = document.getElementById("vuelosTable").getElementsByTagName("tbody")[0];
    setInterval(() => {
        if (table.rows.length > 1) {
            const firstRow = table.rows[0];
            table.appendChild(firstRow);
        }
    }, 2000); // Tiempo rotacion
});
