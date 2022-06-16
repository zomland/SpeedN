using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Helper;

[System.Serializable]
public static class LimitsSpeed
{
    public static Dictionary<string, float[]> CarSpeeds = new Dictionary<string, float[]>()
    {
        {CarSpeedType.Urban.ToString(),new float[]{40f,50f}},
        {CarSpeedType.Suburban.ToString(),new float[]{50f,100f}}
    };
    public static Dictionary<string, float[]> MotobikeSpeeds = new Dictionary<string, float[]>()
    {
        {MotobikeSpeedType.Urban.ToString(),new float[]{40f,50f}},
        {MotobikeSpeedType.Suburban.ToString(),new float[]{50f,80f}}
    };
    public static Dictionary<string, float[]> BicycleSpeeds = new Dictionary<string, float[]>()
    {
        {BicycleSpeedType.Beginner.ToString(),new float[]{12f,15f}},
        {BicycleSpeedType.Advance.ToString(),new float[]{15f,17f}},
        {BicycleSpeedType.Pro.ToString(),new float[]{17f,20f}}
    };
    public static Dictionary<string, float[]> ShoesSpeeds = new Dictionary<string, float[]>()
    {
        {ShoesSpeedType.Walker.ToString(),new float[]{3f,5f}},
        {ShoesSpeedType.Runner.ToString(),new float[]{5f,9f}},
        {ShoesSpeedType.Trainer.ToString(),new float[]{9f,12f}}
    };

    public static float[] LimitSpeed(VehicleType vehicleType, string typeSpeed)
    {
        switch (vehicleType)
        {
            case VehicleType.Car:
                return CarSpeeds[typeSpeed];
            case VehicleType.Motobike:
                return CarSpeeds[typeSpeed];
            case VehicleType.Bicycle:
                return CarSpeeds[typeSpeed];
            case VehicleType.Shoes:
                return CarSpeeds[typeSpeed];
            default:
                return new float[] { 0f, 100f };
        }
    }
}

public enum CarSpeedType { [StringValue("Car_Urban")] Urban, [StringValue("Car_Suburban")] Suburban }
public enum MotobikeSpeedType { [StringValue("Motobike_Urban")] Urban, [StringValue("Motobike_Suburban")] Suburban }
public enum BicycleSpeedType
{
    [StringValue("Bicycle_Beginner")] Beginner,
    [StringValue("Bicycle_Beginner")] Advance, [StringValue("Bicycle_Beginner")] Pro
}
public enum ShoesSpeedType
{
    [StringValue("Shoes_Walker")] Walker,
    [StringValue("Shoes_Runner")] Runner,
    [StringValue("Shoes_Trainer")] Trainer
}

