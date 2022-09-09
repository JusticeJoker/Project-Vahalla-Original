using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTarget : MonoBehaviour
{
    public LayerMask hitLayers;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //If the player clicks left mouse button
        {
            Vector3 mouse = Input.mousePosition; //Get the mouse position
            Ray castPoint = Camera.main.ScreenPointToRay(mouse); //Cast a ray to get where the mouse is at
            RaycastHit hit; //Stores the position where the ray hit
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers)) //If the raycast doesnt hit the wall
                this.transform.position = hit.point; //Move the NPC to the mouse position
        }
    }
}
