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

public class Galaxy
{
    public List<StarSystem> StarSystems { get; set; }

    public Galaxy() 
    {}
}

[Serializable]
public class StarSystem
{
    public string Name              { get; set; }
    public float Xlocation          { get; set; }
    public float Ylocation          { get; set; }
    public List<Planet> Planets     { get; set; }
    public Star Star                { get; set; }

    public StarSystem(string _name, float _xLocation, float _yLocation, List<Planet> _planets, Star _star) 
    {
        Name = _name;
        Xlocation = _xLocation;
        Ylocation = _yLocation;
        Planets = _planets;
        Star = _star;
    }
}

public class Planet
{
    public string Name {get; set;}
    public PlanetType PlanetType    { get; set; }
    public float DistanceFromStar   { get; set; }
    public float Radius           { get; set; }

    public Planet(string _name, PlanetType _type, float _distanceFromStar, float _radius)
    {
        Name = _name;
        PlanetType = _type;
        //this.IsInhabited = _inhabited;
        //this.TotalPopulation = _population;
        //this.SpeciesList = _list;
        DistanceFromStar = _distanceFromStar;
        Radius = _radius;
    }
}

public class Star
{
    public string Name {get; set;}
    
    public StarType Type {get; set;}

    public Color32 Color { 
        get {
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

public class Species
{
    // Traits = BehaviorTrait.Fearful | BehaviorTrait.Hostile
    PersonalityTraits Traits;
    Planet NativePlanet;
}