using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    float startingSize = 5;
    float finalSize = 7, finalDepth = 40;
    int elapsedTime = 0, depth = 12;
    float totalTime = 400;
    float targetSize = 5;
    [SerializeField] float zoomSpeed, cameraSpeed = 10;
    [SerializeField] Trunk tree;
    [SerializeField] AudioClip audioClip; 
    [SerializeField] GameObject muteButton, unMuteButton;
    Camera camera;
    AudioSource audioSource;
    bool zoomingOut = false;
    bool mute = false;
    int screenWidth, screenHeight;

    void Start() 
    {
        camera = GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        StartCoroutine(PlayClips());
    }

    IEnumerator PlayClips()
    {
        if (mute)
            audioSource.mute = true;
        audioSource.Play();
        yield return new WaitForSeconds(120);
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    } 
    void Update()
    {
        audioSource.mute = mute;
        depth = tree.GetDepth();
        MoveCamera();
    }

    void MoveCamera() 
    {
        float hMov = Input.GetAxisRaw("Horizontal");
        float vMov = Input.GetAxisRaw("Vertical");
        float deltaZoom = Input.mouseScrollDelta.y;
        if (deltaZoom != 0)
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize + deltaZoom*zoomSpeed, 3, 12);
        if (hMov != 0 || vMov != 0)
        {
            Vector3 newPos = transform.position + new Vector3(hMov*cameraSpeed, vMov*cameraSpeed, 0);
            newPos.x = Mathf.Clamp(newPos.x, -depth/4, depth/4);
            newPos.y = Mathf.Clamp(newPos.y, -depth/2, 0);
            transform.position = newPos;
        }
        else 
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 movement = transform.position;
            if (mousePos.x < screenWidth / 7)
            {
                movement.x += -1 * cameraSpeed;
            }
            else if (mousePos.x > screenWidth - screenWidth/7)
            {
                movement.x += 1 * cameraSpeed;
            }
            if (mousePos.y > screenHeight - screenHeight / 7)
            {
                movement.y += 1 * cameraSpeed;
            }
            else if (mousePos.y < screenHeight/7)
            {
                movement.y += -1 * cameraSpeed;
            }
            movement.x = Mathf.Clamp(movement.x, -depth/4, depth/4);
            movement.y = Mathf.Clamp(movement.y, -depth/2, 0);
            transform.position = movement;            
        }
    }
    void ZoomOut()
    {
        int newDepth = tree.GetDepth();
        if (!zoomingOut && newDepth > depth)
        {
            targetSize = 5f + (newDepth - 12f) / 4f;
            startingSize = camera.orthographicSize;
            elapsedTime = 0;
            depth = newDepth;
            zoomingOut = true;
        }

        float delta = Mathf.Lerp(startingSize, targetSize, elapsedTime++/totalTime);
        camera.orthographicSize = delta;
        transform.position += new Vector3(0,-zoomSpeed, 0);
        if (elapsedTime == totalTime)
            zoomingOut = false;
    }
    public void Mute()
    {
        // mute = true;
        AudioListener.volume = 0;
        muteButton.SetActive(false);
        unMuteButton.SetActive(true);
    }
    public void UnMute()
    {
        // mute = false;
        AudioListener.volume = 1;
        muteButton.SetActive(true);
        unMuteButton.SetActive(false);
    }
}
