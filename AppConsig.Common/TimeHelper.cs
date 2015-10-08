using System;
using System.Globalization;

namespace AppConsig.Common
{
    public class TimeHelper
    {
        public static string GetDiffTime(DateTime reference)
        {
            var culture = new CultureInfo("pt-BR");
            var now = DateTime.Now;
            var dayReference = reference.Day;
            var monthReference = reference.Month;
            var yearReference = reference.Year;
            var dayNow = now.Day;
            var monthNow = now.Month;
            var yearNow = now.Year;
            var today = dayReference == dayNow && monthReference == monthNow && yearReference == yearNow;
            var yesterday = dayReference == dayNow - 1 && monthReference == monthNow && yearReference == yearNow;
            var diff = (int)now.Subtract(reference).TotalMinutes;

            if (!today)
                return yesterday
                    // Ontem 4 de setembro
                    ? $"Ontem {reference.ToString("%d")} de {culture.DateTimeFormat.GetMonthName(reference.Month)}"
                    // 3 de setembro de 2015
                    : $"{reference.ToString("%d")} de {culture.DateTimeFormat.GetMonthName(reference.Month)} de {reference.Year}";

            // Hoje 5 de setembro
            if (diff >= 12 * 60 && diff < 24 * 60)
            {
                return
                    $"Hoje {reference.ToString("%d")} de {culture.DateTimeFormat.GetMonthName(reference.Month)}";
            }

            // 10 horas atrás
            if (diff >= 2 * 60 && diff < 12 * 60)
            {
                return $"{diff / 60} horas atrás";
            }

            // 1 hora atrás
            if (diff >= 60 && diff < 2 * 60)
            {
                return $"{diff / 60} hora atrás";
            }

            // 30 minutos atrás
            if (diff < 60 && diff >= 2)
            {
                return $"{diff} minutos atrás";
            }

            // 1 minuto atrás
            if (diff < 2 && diff > 0)
            {
                return $"{diff} minuto atrás";
            }

            // Agora
            return "Agora";
        }
    }
}