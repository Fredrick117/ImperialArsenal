using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SystemType
{
    Singular,
    Binary,
    Trinary,
    Quaternary
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
    Water,
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

public class StarSystem
{
    public string Name             { get; set; }
    public SystemType Type         { get; set; }
    public float Xlocation         { get; set; }
    public float Ylocation         { get; set; }
    public List<Object> Objects    { get; set; }

    public StarSystem(string _name, SystemType _type, float _xLocation, float _yLocation, List<Object> _objects) 
    {
        Name = _name;
        Type = _type;
        Xlocation = _xLocation;
        Ylocation = _yLocation;
        Objects = _objects;
    }

    public void AddObjectToStarSystem(Object o)
    {
        Objects.Add(o);
    }
}

public class Object
{
    public string Name { get; set; }
    public float Xlocation { get; set; }
    public float Ylocation { get; set; }
}

public class Planet : Object
{
    public PlanetType PlanetType     { get; set; }
    public float DistanceFromStar    { get; set; }

    public Planet(string _name, PlanetType _type, float _distanceFromStar, float _Xlocation, float _Ylocation)
    {
        Name = _name;
        PlanetType = _type;
        //this.IsInhabited = _inhabited;
        //this.TotalPopulation = _population;
        //this.SpeciesList = _list;
        DistanceFromStar = _distanceFromStar;
        Xlocation = _Xlocation;
        Ylocation = _Ylocation;
    }
}

public class Star : Object
{
    public Color Color { get; set; }

    public Star(string _name, float _Xlocation, float _Ylocation, Color _color)
    {
        Name = _name;
        Xlocation = _Xlocation;
        Ylocation = _Ylocation;
        Color = _color;
    }
}

public class Species
{
    // Traits = BehaviorTrait.Fearful | BehaviorTrait.Hostile
    PersonalityTraits Traits;
    Planet NativePlanet;
}