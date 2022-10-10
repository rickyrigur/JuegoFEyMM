using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camara : MonoBehaviour
{
    WebCamTexture camTexture;
    public string path;
    public RawImage imgDisplay;
    public Text textoCamOnOff;


    public void StartStopCamera()
    {
        if(camTexture != null)
        {
            imgDisplay.texture = null;
            camTexture.Stop();
            camTexture = null;

            textoCamOnOff.text = "Camera ON";
        }
        else
        {
            WebCamDevice device = WebCamTexture.devices[0];
            camTexture = new WebCamTexture(device.name);
            imgDisplay.texture = camTexture;

            camTexture.Play();

            textoCamOnOff.text = "Camera OFF";
        }
    }

    void Start()
    {
        //camTexture = new WebCamTexture();
        //camTexture.Play();
        //gameObject.GetComponent<SpriteRenderer>().material.mainTexture = camTexture;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
