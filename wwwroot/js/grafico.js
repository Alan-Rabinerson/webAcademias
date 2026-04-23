document.addEventListener("DOMContentLoaded", function () {
  google.charts.load("current", { packages: ["bar"] });
  google.charts.setOnLoadCallback(drawStuff);

  function drawStuff() {
    var rawData = window.porcentajesData;
    var dataArray = [["Materia", "Porcentaje"]];
    for (var key in rawData) {
      dataArray.push([key, rawData[key]]);
    }
    var data = google.visualization.arrayToDataTable(dataArray);

    var options = {
      title: "Chess opening moves",
      width: 900,
      legend: { position: "none" },
      chart: { title: "Materias más ofrecidas en nuestros centros asociados" },
      bars: "horizontal", // Required for Material Bar Charts.
      axes: {
        x: {
          0: { side: "top", label: "Porcentaje" }, // Top x-axis.
        },
      },
      bar: { groupWidth: "90%" },
    };

    var chart = new google.charts.Bar(
      document.getElementsByClassName("grafico")[0],
    );
    chart.draw(data, options);
  }
});
