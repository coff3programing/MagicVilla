using System;
using MagicVilla_API.DTO;

namespace MagicVilla_API.Datos;

public class VillaStore
{
    public static List<VillaDTO> villaList = new List<VillaDTO>
    {
        new VillaDTO{Id=1,Name="Vista a la piscina", Occupants=3, SquareMeters=50},
        new VillaDTO{Id=2,Name="Vista a la playa", Occupants=4, SquareMeters=80},
    };
}
