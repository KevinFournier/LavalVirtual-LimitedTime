﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType { Gravel, Grass, Wood, Cobblestone }
public class Ground : MonoBehaviour
{
    public GroundType Type = GroundType.Gravel;
}

