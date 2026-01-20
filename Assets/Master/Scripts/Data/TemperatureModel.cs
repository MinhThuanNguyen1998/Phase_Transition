using System.Collections.Generic;
using UnityEngine;

public class TemperatureModel 
{
    // Temperature Text
    public const string Temperature_Celcius = "°C";
    public const string Temperature_Kelvin = "K";
    public const string DEFAULT_TEXT = "ON";

    // Radius Value
    public static float Radius = 0.04f;
    public const float DefaultRadius = 0.04f;
    public const float MIN_RADIUS = 1.04f;
    public const float MAX_RADIUS = 6.04f;

    // Temperature Value
    public const int MIN_TEMP = 20;
    public const int MAX_TEMP = 260;
    public const int STEP_TEMP = 40;
    public static float TEMPERATURE = 20f;

    // Point
    public const float POINT_SPEED_ON_MOLECULE_CREATION = 0.09f;
    public const float POINT_SPEED_ON_SWITCH_MODE = 0.35f;
    public const float POINT_SPEED_ON_CHANGE_TEMPERATURE = 0.12f;
    public const float MIN_POINT_SPEED = 0.12f;
    public const float MAX_POINT_SPEED = 1f;

    public static void OnTemperatureChanged(int delta)
    {
        Radius = Mathf.Clamp(Radius + Mathf.Sign(delta),MIN_RADIUS,MAX_RADIUS);
    }
    public static void SetGasState()
    {
        Radius = MAX_RADIUS;
    }
    public static int GetStateIndex()
    {
        if (Mathf.Approximately(Radius, DefaultRadius))
            return 0; // Solid

        if (Radius > DefaultRadius && Radius <= MIN_RADIUS)
            return 1; // Liquid
        return 2; // Gas
    }
}
