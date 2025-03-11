using UnityEngine;

public class Door : MonoBehaviour, IPowerable
{
    private Vector3 endPos;
    private Vector3 startPos;

    public float doorMoveSpeed = 0.2f;

    public bool powered = false;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, 3, 0);
    }

    private void FixedUpdate()
    {
        if (powered)
        {
            if (transform.position != endPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, doorMoveSpeed);
            }
        }
        else
        {
            if (transform.position != startPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, doorMoveSpeed);
            }
        }
    }

    public void OnPower()
    {
        powered = true;
    }

    public void OnUnpower()
    {
        powered = false;
    }


}
