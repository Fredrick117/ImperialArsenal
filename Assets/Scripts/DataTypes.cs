using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarType
{
    M,
    K,
    G,
    F,
    A,
    B,
    O
}

public enum ObjectType
{
    HabitablePlanet,
    Star,
}

public enum PlanetType
{
    Arctic,
    Lava,
    Terrestrial,
    Desert,
    GasGiant,
    Ocean,
}

[Serializable]
public class StellarObject
{
    public string Name { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}

[Serializable]
public class Planet : StellarObject
{
    public PlanetType PlanetType;
    public Color32 Color
    {
        get
        {
            return PlanetType switch
            {
                PlanetType.Arctic => new Color32(200, 219, 250, 255),
                PlanetType.Lava => new Color32(255, 80, 36, 255),
                PlanetType.Terrestrial => new Color32(72, 150, 74, 255),
                PlanetType.Desert => new Color32(235, 214, 141, 255),
                PlanetType.Ocean => new Color32(24, 69, 122, 255),
                PlanetType.GasGiant => new Color32(147, 83, 212, 255),
                _ => new Color32(255, 5, 230, 255),
            };
        }
    }

    public Planet(string _name, PlanetType _type, float _x, float _y)
    {
        Name = _name;
        PlanetType = _type;
        X = _x;
        Y = _y;
    }
}

public class Star : StellarObject
{
    public StarType Type { get; set; }
    public float Radius { get; set; }

    public Color32 Color
    {
        get
        {
            return Type switch
            {
                StarType.M => new Color32(237, 30, 30, 255),
                StarType.K => new Color32(237, 116, 30, 255),
                StarType.G => new Color32(255, 220, 48, 255),
                StarType.F => new Color32(255, 255, 255, 255),
                StarType.A => new Color32(135, 219, 255, 255),
                StarType.B => new Color32(45, 152, 252, 255),
                StarType.O => new Color32(0, 33, 255, 255),
                _ => new Color32(255, 255, 255, 255),
            };
        }
    }

    public Star(string _name, float _radius, StarType _type)
    {
        Name = _name;
        Radius = _radius;
        Type = _type;
    }
}