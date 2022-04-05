using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Galaxy
{
    public List<StarSystem> StarSystems;
}

[System.Serializable]
public enum SystemType
{
    Singular,
    Binary,
    Trinary,
    Quaternary
}

[System.Serializable]
public class StarSystem
{
    public SystemType Type;
    public float xLocation;
    public float yLocation;
    public List<Object> CelestialObjects;
}

[System.Serializable]
public enum ObjType
{
    HabitablePlanet,
    Star,
}

[System.Serializable]
public class Object
{
    public float xLocation;
    public float yLocation;
}

[System.Serializable]
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

[System.Serializable]
public class Planet : Object
{
    public PlanetType Type;
    public bool IsInhabited;
    public List<Species> SpeciesList;
    public int TotalPopulation;
    public float DistanceFromStar;
}

[System.Serializable]
public class Star : Object
{
    public StarSystem StarSystem;
}

[System.Serializable]
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

[System.Serializable]
public class Species
{
    // Traits = BehaviorTrait.Fearful | BehaviorTrait.Hostile
    PersonalityTraits Traits;
    Planet NativePlanet;
}