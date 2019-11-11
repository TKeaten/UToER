using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public CharacterController2D player;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player)
        {
            Debug.Log("Womp womp");
            player.Die();
            player.GameOver();
        }
    }
}
