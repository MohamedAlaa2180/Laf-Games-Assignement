using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject arrow, singleUnit;
    [SerializeField] bool rayCasting , move;
    GameObject hitted;
    Vector3 arrowDirection;
    string direction;
    float distance;
    BoxCollider col;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        arrow = transform.GetChild(0).gameObject;
        EditArrows();
    }
    
    // EditArrows() : editing arrows direction depending on the position

    void EditArrows()
    {
        if (transform.localPosition.x == 0)
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
            arrowDirection = transform.TransformDirection(Vector3.right);
            direction = "right";
        }
        else if(transform.localPosition.x == GridController.Instance.GetWidth())
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
            arrowDirection = transform.TransformDirection(Vector3.left);
            direction = "left";
        }
        else if(transform.localPosition.y == 0)
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
            arrowDirection = transform.TransformDirection(Vector3.down);
            direction = "down";
        }
        else if(transform.localPosition.y == -1 * GridController.Instance.GetHeight())
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, 270);
            arrowDirection = transform.TransformDirection(Vector3.up);
            direction = "up";
        }
    }

    public void CreateBlock()
    {
        if (GridController.Instance.unitSize > distance) // if size of the block is bigger than the available slots , return
        {
            return;
        }
        col.enabled = false;
        
        singleUnit = Instantiate(singleUnit, transform);
        singleUnit.GetComponent<BoxCollider>().enabled = false;
        singleUnit.transform.localPosition = new Vector3(0,0,-0.51f);

        if(direction == "left" || direction == "right") // changing block scale on X or Y depending on the direction
        {
            singleUnit.transform.localScale = new Vector3(GridController.Instance.unitSize, 1, 1);
        }
        else if (direction == "up" || direction == "down")
        {
            singleUnit.transform.localScale = new Vector3(1, GridController.Instance.unitSize, 1);
        }
        move = true;       
    }

    private void Update()
    {
        if(!rayCasting)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, arrowDirection, out hit, Mathf.Infinity , mask))
        {
            if(hit.transform.gameObject.tag == "arrow")
            {
                distance = hit.distance;
                hitted = hit.transform.gameObject;
                Debug.DrawRay(transform.position, arrowDirection * hit.distance, Color.yellow);
            }
            
        }

        if (distance < 1) // if size of the block is bigger than the available slots , return
        {
            Debug.Log("DOne");
            rayCasting = false;
        }

        if (move)
        {
            if (direction == "right")
            {
                if (singleUnit.transform.localPosition.x >= hit.distance - (GridController.Instance.unitSize/2 + 0.01f))
                {
                    singleUnit.GetComponent<BoxCollider>().enabled = true;
                   // rayCasting = false;
                    col.enabled = true;
                    return;
                }
                singleUnit.transform.Translate(arrowDirection * 0.05f);
            }
            else if (direction == "left")
            {
                if (singleUnit.transform.localPosition.x <= -hit.distance + (GridController.Instance.unitSize / 2 + 0.01f))
                {
                    singleUnit.GetComponent<BoxCollider>().enabled = true;
                   // rayCasting = false;
                    col.enabled = true;
                    return;
                }
                singleUnit.transform.Translate(arrowDirection * 0.05f);
            }
            else if (direction == "up")
            {
                if (singleUnit.transform.localPosition.y >= hit.distance - (GridController.Instance.unitSize / 2 + 0.01f))
                {
                    singleUnit.GetComponent<BoxCollider>().enabled = true;
                   // rayCasting = false;
                    col.enabled = true;
                    return;
                }
                singleUnit.transform.Translate(arrowDirection * 0.05f);
            }
            else if (direction == "down")
            {
                if (singleUnit.transform.localPosition.y <= -hit.distance + (GridController.Instance.unitSize / 2 + 0.01f))
                {
                    singleUnit.GetComponent<BoxCollider>().enabled = true;
                   // rayCasting = false;
                    col.enabled = true;
                    return;
                }
                singleUnit.transform.Translate(arrowDirection * 0.05f);
            }
        }

        

    }
}
    