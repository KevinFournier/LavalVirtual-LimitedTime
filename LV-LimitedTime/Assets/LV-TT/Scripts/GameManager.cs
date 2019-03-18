using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tags
{
    public const string Ground = "Ground";
}

public enum Mode { Normal, Blind }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Mode Mode;

    public WindZone WindZone;

    private void Awake()
    {
        Instance = this;
    }

}