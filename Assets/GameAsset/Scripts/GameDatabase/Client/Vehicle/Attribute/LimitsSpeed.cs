using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimitsSpeed
{
    public Dictionary<CarType, float[]> Car = new Dictionary<CarType, float[]>()
    {
        {CarType.Urban,new float[]{40f,50f}},
        {CarType.Suburban,new float[]{50f,100f}}
    };
    public Dictionary<MotobikeType, float[]> Motobike = new Dictionary<MotobikeType, float[]>()
    {
        {MotobikeType.Urban,new float[]{40f,50f}},
        {MotobikeType.Suburban,new float[]{50f,80f}}
    };
    public Dictionary<BicycleType, float[]> Bicycle = new Dictionary<BicycleType, float[]>()
    {
        {BicycleType.Beginner,new float[]{12f,15f}},
        {BicycleType.Advance,new float[]{15f,17f}},
        {BicycleType.Pro,new float[]{17f,20f}}
    };
    public Dictionary<ShoesType, float[]> Shoes = new Dictionary<ShoesType, float[]>()
    {
        {ShoesType.Walker,new float[]{3f,5f}},
        {ShoesType.Runner,new float[]{5f,9f}},
        {ShoesType.Trainer,new float[]{9f,12f}}
    };
    public LimitsSpeed() { }
}
