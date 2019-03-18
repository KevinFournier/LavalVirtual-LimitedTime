using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tags
{
    public const string Ground = "Ground";
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public WindZone WindZone;

    private void Awake()
    {
        Instance = this;
    }

}