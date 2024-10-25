using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnterData : MonoBehaviour
{
    public static TravelType travelType = TravelType.Nothing;
}

public enum TravelType
{
    Enter,
    Exit,
    Teleport,
    Nothing
}
