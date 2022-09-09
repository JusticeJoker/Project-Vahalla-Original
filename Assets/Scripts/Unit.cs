using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GameObject selectUnit;
    private Color npcSelection;
    public bool selected = false;
    public float speed = 5;

    public float floorOffset = 1;
    public float stopDistanceOffset = 0.5f;

    private Vector3 moveToDest = Vector3.zero;


    //selectUnit = transform.Find("CheckSelection").gameObject;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 cameraPosition = Camera.main.WorldToScreenPoint(transform.position);
            cameraPosition.y = NPCControllerTest.InvertMouseY(cameraPosition.y);
            selected = NPCControllerTest.selection.Contains(cameraPosition);
        }

        if (selected)
            GetComponent<Renderer>().material.color = Color.red;
        else
            GetComponent<Renderer>().material.color = Color.white;

        if (selected && Input.GetMouseButtonUp(1))
        {
            /*Vector3 destination = NPCControllerTest.GetDestination();

            if (destination != Vector3.zero)
            {
                // gameObject.GetComponent<NavMeshAgent>().SetDestination(destination)

                moveToDest = destination;
                moveToDest.y += floorOffset;
            }*/
        }

        UpdateMove();
    }

    private void UpdateMove()
    {
        /*if (moveToDest != Vector3.zero && transform.position != moveToDest)
        {
            Vector3 direction = (moveToDest - transform.position).normalized;
            direction.y = 0;
            transform.GetComponent<Rigidbody>().velocity = direction * speed;

            if (Vector3.Distance(transform.position, moveToDest) < stopDistanceOffset)
                moveToDest = Vector3.zero;
        }
        else
        {
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }*/
            
            
    }
}
