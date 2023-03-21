using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if(player = collision.GetComponent<Player>())
        {
            FindObjectOfType<LevelUI>().ShowFinishUI(true);
        }
    }
}
