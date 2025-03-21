using System.Collections.Generic;
using UnityEngine;

public class PlayerInputRecorder : MonoBehaviour
{
    [System.Serializable]
    public class InputFrame
    {
        public float relativeTime;
        public float horizontal;
        public bool jump;
    }

    public List<InputFrame> recordedInputs = new List<InputFrame>();
    private float runStartTime;
    private bool jumpPressed;

    void Start()
    {
        runStartTime = Time.time;
    }

    void Update()
    {
        // Capture jump input in Update
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance != null && GameManager.Instance.playerControlsEnabled)
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        InputFrame frame = new InputFrame();
        // Record time relative to the start of this run.
        frame.relativeTime = Time.time - runStartTime;
        if (GameManager.Instance != null && GameManager.Instance.playerControlsEnabled)
        {
            frame.horizontal = Input.GetAxis("Horizontal");
            if (jumpPressed)
            {
                frame.jump = true;
                jumpPressed = false;
            }
            else frame.jump = false;
        }
        else
        {
            frame.horizontal = 0;
            frame.jump = false;
        }
        
        recordedInputs.Add(frame);
    }
}
