using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public List<GameObject> targets;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool isPressed = false;

    private AudioSource press;


    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, -0.2f, 0);
        press = gameObject.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (isPressed)
        {
            if (transform.position != endPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, 1f * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position != startPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, 1f * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isPressed) press.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isPressed = true;

        foreach (var target in targets)
        {
            if (target.TryGetComponent(out IPowerable obj))
            {
                obj.OnPower();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        isPressed = false;

        foreach (var target in targets)
        {
            if (target.TryGetComponent(out IPowerable obj))
            {
                obj.OnUnpower();
            }
        }
    }
}
