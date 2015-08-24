/* Geolocation */
/* Does your browser support geolocation? */
if ("geolocation" in navigator) {
    $('#weather').show();
} else {
    $('#weather').hide();
}

$(document).ready(function () {
    CheckMenuCompact();
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
            //<i class="weather-icon-0"></i>
            var html = '';
            html += weather.temp + '&deg;' + weather.units.temp;
            html += '<i class="weather-icon-' + weather.code + '"></i>';

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
    createCookie("menu-compact", $('.page-sidebar').hasClass('menu-compact'), 100);
});

function CheckMenuCompact() {
    if (readCookie("menu-compact") != null) {
        if (readCookie("menu-compact") === "true") {
            $('.page-sidebar').addClass('menu-compact');
            $('.sidebar-collapse').addClass('active');
        }
    }
}
/* End Sidebar Collapse */