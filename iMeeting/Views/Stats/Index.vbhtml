@Code
    ViewData("Title") = "Index"
End Code

@Section css
    @Styles.Render("~/Content/css/graph")
End Section

<div class="col-sm-6 col-md-6">
    <div style="height: 500px; width : 600px">
        <h3>Proportion des réunions par emplacement</h3><hr />
        <svg id="chart2"></svg>
    </div>
</div>
<div class="col-sm-6 col-md-6">
    <div id="chart3">
        <h3> Nombre de reservations de salles par services </h3>
        <svg style="height: 230px; width : 500px"></svg>
    </div>
    <div id="chart4">
        <h3> Nombre de reservations d'espaces par services </h3>
        <svg style="height: 230px; width : 500px"></svg>
    </div>
</div>

@Section Scripts
    @Scripts.Render("~/bundles/graph")


<script>

    // piechart
    var testdata = @Html.Raw(Json.Encode(ViewBag.pieData));
    

    //en batons
    historicalBarChart = [
      {
          key: "Cumulative Return",
          values: @Html.Raw(Json.Encode(ViewBag.bar1Data))
      }
    ];

    historicalBarChart2 = [
      {
          key: "Cumulative Return",
          values: @Html.Raw(Json.Encode(ViewBag.bar2Data))
      }
    ];


    ////En serie
    //var testdata2 = [
    //  {
    //    "key" : "Espaces" ,
    //    "values": [[, 7], [, 3], [, 9], [, 10], [, 0]]
    //  }
    //].map(function(series) {
    //  series.values = series.values.map(function(d) { return {x: d[0], y: d[1] } });
    //  return series;
    //});


    //piechart
    nv.addGraph(function () {
        var width = 450,
            height = 500;

        var chart = nv.models.pieChart()
            .x(function (d) { return d.key })
            .y(function (d) { return d.y })
            .color(d3.scale.category10().range())
            .width(width)
            .height(height);

        d3.select("#chart2")
            .datum(testdata)
          .transition().duration(1200)
            .attr('width', width)
            .attr('height', height)
            .call(chart);

        chart.dispatch.on('stateChange', function (e) { nv.log('New State:', JSON.stringify(e)); });

        return chart;
    });

    // en batons1
    nv.addGraph(function () {
        var chart = nv.models.discreteBarChart()
            .x(function (d) { return d.label })
            .y(function (d) { return d.value })
            .staggerLabels(true)

            //.staggerLabels(historicalBarChart[0].values.length > 8)
            .tooltips(true)
            .showValues(true)
            .transitionDuration(250)
        ;

        d3.select('#chart3 svg')
            .datum(historicalBarChart)
            .call(chart);

        nv.utils.windowResize(chart.update);

        return chart;
    });

    // en batons2
    nv.addGraph(function () {
        var chart = nv.models.discreteBarChart()
            .x(function (d) { return d.label })
            .y(function (d) { return d.value })
            .staggerLabels(true)
            //.staggerLabels(historicalBarChart[0].values.length > 8)
            .tooltips(true)
            .showValues(true)
            .transitionDuration(250)
        ;

        d3.select('#chart4 svg')
            .datum(historicalBarChart2)
            .call(chart);

        nv.utils.windowResize(chart.update);

        return chart;
    });



    ////en serie
    //var chart;

    //nv.addGraph(function () {
    //    chart = nv.models.linePlusBarChart()
    //        .margin({ top: 30, right: 60, bottom: 50, left: 70 })
    //        .x(function (d, i) { return i })
    //        .color(d3.scale.category10().range());

    //    chart.xAxis.tickFormat(function (d) {
    //        var dx = testdata2[0].values[d] && testdata2[0].values[d].x || 0;
    //        return dx ? d3.time.format('%x')(new Date(dx)) : '';
    //    })
    //      .showMaxMin(false);

    //    chart.y1Axis
    //        .tickFormat(d3.format(',f'));

    //    chart.y2Axis
    //        .tickFormat(function (d) { return '$' + d3.format(',.2f')(d) });

    //    chart.bars.forceY([0]).padData(false);
    //    //chart.lines.forceY([0]);

    //    d3.select('#chart4 svg')
    //        .datum(testdata2)
    //      .transition().duration(500).call(chart);

    //    nv.utils.windowResize(chart.update);

    //    chart.dispatch.on('stateChange', function (e) { nv.log('New State:', JSON.stringify(e)); });

    //    return chart;
    //});



</script>


End Section


