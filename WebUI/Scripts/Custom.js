google.charts.load('current', { 'packages': ['bar'] });
google.charts.setOnLoadCallback(drawChart);
function drawChart() {
    var data = google.visualization.arrayToDataTable([
      ['Måned', 'Emails pr. Måned'],
      ['Januar', 47],
      ['Februar', 86],
      ['Marts', 32],
      ['April', 90],
      ['Maj', 99],
      ['Juni', 45],
      ['Juli', 54],
      ['August', 66],
      ['September', 55],
      ['Oktober', 100],
      ['November', 40],
      ['December', 76]
    ]);

    var options = {
        chart: {
            title: 'Email aktivitet'
        }
    };

    var chart = new google.charts.Bar(document.getElementById('columnchart_material2z'));

    chart.draw(data, options);

}