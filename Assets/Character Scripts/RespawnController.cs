using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class RespawnController : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject character;

    private bool showSprite;
    private const int blinkTimes = 4;
    private int currentBlink;

    public async Task Blink()
    {
        showSprite = true;

        currentBlink = blinkTimes * 2 + 1;
        //two because how eveer many times it needs to blink it needs to switch back
        //even off
        //odd on
        while (currentBlink > 0)
        {
            await Task.Delay(250);

            //on
            character.SetActive(showSprite);
            //switch
            showSprite = !showSprite;
            //decrease blink
            currentBlink--;
        }
    }

   [ContextMenu("Respawn")]
   public async void Respawn()
   {
        //rotate
        //to do organize delagets


        showSprite = false;
        //off
        character.SetActive(false);

        //move
        character.transform.position = new Vector2(respawnPoint.position.x, respawnPoint.position.y); ;
        //start blink

        await Blink();
    }



}
