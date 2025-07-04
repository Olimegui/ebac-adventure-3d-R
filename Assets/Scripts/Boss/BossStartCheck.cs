using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string tagToCheck = "Player";

    public GameObject BossCamera;
    public Color gizmoColor = Color.yellow;

    private void Awake()
    {
        BossCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheck)
        {
            TurnCameraOn();
        }
    }

    private void TurnCameraOn()
    {
        BossCamera.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
