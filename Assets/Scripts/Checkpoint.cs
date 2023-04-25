using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public enum CheckpointType { LevelStart, ScreenStart, ScreenEnd, LevelEnd };
    public CheckpointType checkpointType;

    public Transform nextCheckpoint;
}
