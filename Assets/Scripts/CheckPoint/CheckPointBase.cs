using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActived = false;
    private string checkpointkey = "CheckpointKey";

    private void OnTriggerEnter(Collider other)
    {
        if (checkpointActived && other.transform.tag == "Player")
        {
          CheckCheckpoint();

        }
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
        SaveCheckPoint();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnItOf()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckPoint()
    {
        if(PlayerPrefs.GetInt(checkpointkey, 0) > key)
        PlayerPrefs.SetInt(checkpointkey, key);

        checkpointActived = true;
    }
}
