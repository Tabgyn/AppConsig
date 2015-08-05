/* Geolocation */
/* Does your browser support geolocation? */
if ("geolocation" in navigator) {
    $('#weather').show();
} else {
    $('#weather').hide();
}

$(document).ready(function () {
    getWeather(); //Get the initial weather.
    setInterval(getWeather, 600000); //Update the weather every 10 minutes.
});

function getWeather() {
    navigator.geolocation.getCurrentPosition(function (position) {
        loadWeather(position.coords.latitude + ',' + position.coords.longitude); //load weather using your lat/lng coordinates
    });
}

function loadWeather(location, woeid) {
    $.simpleWeather({
        location: location,
        woeid: woeid,
        unit: 'c',
        success: function (weather) {
            //18°c
            //<i class="wi wi-cloudy"></i>
            var html = '';
            html += weather.temp + '&deg;' + weather.units.temp;
            html += '<i class="fa weather-icon-' + weather.code + ' fa-fw"></i>';

            $("#weather").html(html);
        },
        error: function (error) {
            $("#weather").html('<p>' + error + '</p>');
            $('#weather').hide();
        }
    });
}
/* End Geolocation */

/* Sidebar Collapse */
$("#sidebar-collapse").on('click', function () {
    $(".collapse-icon").toggleClass("fa-angle-double-left");
    $(".collapse-icon").toggleClass("fa-angle-double-right");
});
/* End Sidebar Collapse */