using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCar : MonoBehaviour
{
    public bool isPlaying = true;
    public RectTransform[] LinesRect;
    public RectTransform CarRect;
    public float timeLoopLines;
    float speedCar;

    float timeRun;
    const int numLines = 3;
    float[] speedLines = new float[numLines];


    void Start()
    {
        speedCar = (Screen.width + CarRect.rect.width) / 168;
    }

    void BackToStartPosition()
    {
        foreach (RectTransform rectTf in LinesRect)
        {
            Vector3 newPosition = new Vector3(CarRect.position.x -
            Random.Range(CarRect.rect.width * 5, CarRect.rect.width * 7)
            , rectTf.position.y, rectTf.position.z);
            rectTf.position = newPosition;
        }

        for (int i = 0; i < 3; i++)
        {
            speedLines[i] = Random.Range(5f, 20f);
        }
    }

    void CarMotion()
    {
        Vector3 newPosition = new Vector3(CarRect.position.x + speedCar
        , CarRect.position.y, CarRect.position.z);
        CarRect.position = newPosition;
    }

    void LinesMotion()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 newPosition = new Vector3(LinesRect[i].position.x - speedLines[i]
            , LinesRect[i].position.y, LinesRect[i].position.z);
            LinesRect[i].position = newPosition;
        }
    }

    void PlayAnimation()
    {
        if (isPlaying)
        {
            timeRun += Time.fixedDeltaTime;
            CarMotion();
            LinesMotion();
            if (timeRun >= timeLoopLines)
            {
                BackToStartPosition();
                timeRun = 0;
            }
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PlayAnimation();
    }
}
