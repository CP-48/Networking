using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{

    [SyncVar(hook = nameof(OnHolaCountChanged))]
    int holaCount = 0;

   void HandleMovement()
   {
       if(isLocalPlayer)
       {
           float moveHorizontal = Input.GetAxis("Horizontal");
           float moveVertical = Input.GetAxis("Vertical");
           Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
           transform.position += movement;
       }
   }

    void Update()
    {
       HandleMovement();

       if(isLocalPlayer && Input.GetKeyDown(KeyCode.X))
       {
           Debug.Log("Sending hola to server!");
           Hola();
       }

       /*if(isServer && transform.position.y > 50)
       {
           TooHigh();
       }*/
    }

    public override void OnStartServer()
    {
        Debug.Log("Player has been spawned on the server!");
    }

    [Command]
    void Hola()
    {
        Debug.Log("Received hola from client!");
        holaCount += 1;
        ReplyHola();
    }

    [TargetRpc]
    void ReplyHola()
    {
        Debug.Log("Received hola from server!");
    }    

    void TooHigh()
    {
        Debug.Log("Too high!");
    }

    void OnHolaCountChanged(int oldCount, int newCount)
    {
        Debug.Log($"We had {oldCount} holas, but now we have {newCount} holas");
    }

}
