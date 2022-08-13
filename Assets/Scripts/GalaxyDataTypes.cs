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

public enum ObjType
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

[Flags]
public enum PersonalityTraits
{
    Anxious = 1 << 0,
    Hostile = 1 << 1,
    Impulsive = 1 << 2,
    Fearful = 1 << 3,
    Kind = 1 << 4,
    Gregarious = 1 << 5,
    Cheerful = 1 << 6,
    Imaginative = 1 << 7,
    Aesthetic = 1 << 8,
    Emotional = 1 << 9,
    Adventurous = 1 << 10,
    Curious = 1 << 11,
    Trusting = 1 << 12,
    Altruistic = 1 << 13,
    Cooperative = 1 << 14,
    Modest = 1 << 15,
    Competent = 1 << 16,
    Sympathetic = 1 << 17,
};

[Serializable]
public class StarSystem
{
    [SerializeField]
    public string Name          { get; set; }
    public float Xlocation      { get; set; }
    public float Ylocation      { get; set; }
    public SpaceObject[] Objects { get; set; }
    public Star Star            { get; set; }

    public StarSystem(string _name, float _xLocation, float _yLocation, SpaceObject[] _objects, Star _star) 
    {
        Name = _name;
        Xlocation = _xLocation;
        Ylocation = _yLocation;
        Objects = _objects;
        Star = _star;
    }
}

[Serializable]
public class SpaceObject
{
    public string Name { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}

[Serializable]
public class Planet : SpaceObject
{
    public PlanetType PlanetType;
    public float DistanceFromStar;
    public float Radius;
    public Color32 Color
    {
        get
        {
            switch (PlanetType)
            {
                case PlanetType.Arctic:
                    return new Color32(200, 219, 250, 255);
                case PlanetType.Lava:
                   return new Color32(255, 80, 36, 255);
                case PlanetType.Terrestrial:
                    return new Color32(72, 150, 74, 255);
                case PlanetType.Desert:
                    return new Color32(235, 214, 141, 255);
                case PlanetType.Ocean:
                    return new Color32(24, 69, 122, 255);
                case PlanetType.GasGiant:
                    return new Color32(147, 83, 212, 255);
                default:
                    return new Color32(255, 5, 230, 255);
            }
        }
    }

    public Planet(string _name, PlanetType _type, float _distanceFromStar, float _radius)
    {
        Name = _name;
        PlanetType = _type;
        DistanceFromStar = _distanceFromStar;
        Radius = _radius;
    }
}

public class Star : SpaceObject
{
    public StarType Type { get; set; }

    public Color32 Color 
    { 
        get 
        {
            switch (Type) 
            {
                case StarType.M:
                    return new Color32(255, 148, 8, 255);
                case StarType.K:
                    return new Color32(255, 177, 82, 255);
                case StarType.G:
                    return new Color32(255, 227, 105, 255);
                case StarType.F:
                    return new Color32(255, 227, 105, 255);
                case StarType.A:
                    return new Color32(161, 170, 255, 255);
                case StarType.B:
                    return new Color32(217, 220, 255, 255);
                case StarType.O:
                    return new Color32(235, 236, 255, 255);
                default:
                    return new Color32(255, 255, 255, 255);
            }         
        }
    }

    public Star(string _name, StarType _type)
    {
        Name = _name;
        Type = _type;
    }
}