using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public enum CheckpointType { LevelStart, ScreenStart, ScreenEnd, LevelEnd };
    public CheckpointType checkpointType;

    [SerializeField] Transform nextScreenStartPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        switch (checkpointType)
        {
            case CheckpointType.LevelStart:
                //something
                break;

            case CheckpointType.ScreenStart:
                //something
                break;

            case CheckpointType.ScreenEnd:
                var runRestarter = collision.gameObject.GetComponent<RunRestarter>();
                runRestarter.SetScreenStartPoint(nextScreenStartPoint);
                runRestarter.SetPlayerPosition(nextScreenStartPoint);
                break;

            case CheckpointType.LevelEnd:
                //something
                break;
        }
    }
}
