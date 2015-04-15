using UnityEngine;
using System.Collections;

public class ScreenRecorder : MonoBehaviour
{
    public int framerate = 30;
    public int superSize;
    public bool autoRecord;

    int frameCount;
    bool recording;

    void Start ()
    {
        if (autoRecord) StartRecording ();
    }

    void StartRecording ()
    {
        System.IO.Directory.CreateDirectory ("Capture");
        Time.captureFramerate = framerate;
        frameCount = -1;
        recording = true;
    }

    void Update ()
    {
        if (recording)
        {
            if (Input.GetMouseButtonDown (0))
            {
                recording = false;
                enabled = false;
            }
            else
            {
                if (frameCount > 0)
                {
                    var name = "Capture/frame" + frameCount.ToString ("0000") + ".png";
                    Application.CaptureScreenshot (name, superSize);
                }

                frameCount++;

                if (frameCount > 0 && frameCount % 60 == 0)
                {
                    Debug.Log ((frameCount / 60).ToString() + " seconds elapsed.");
                }
            }
        }
    }

    void OnGUI ()
    {
        if (!recording && GUI.Button (new Rect (0, 0, 200, 50), "Start Recording"))
        {
            StartRecording ();
            Debug.Log ("Click Game View to stop recording.");
        }
    }
}
