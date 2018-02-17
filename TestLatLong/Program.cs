using System;

namespace TestLatLong
{
    class Program
    {
        static void Main(string[] args)
        {
            var latitude = 16.243199;
            var lastLatitude = 16.241502;
            var longitude = 103.258251;
            var lastLongitude = 103.260847;
            
            var lastCoOlat = latitude - lastLatitude;
            var lastCoOlong = longitude - lastLongitude;
            var lastCoSqrt = Math.Sqrt(Math.Pow(lastCoOlat, 2) + Math.Pow(lastCoOlong, 2));
            var lastCoDistance = (lastCoSqrt / (1 / 108.4)) * 1000;


        }
    }
}
