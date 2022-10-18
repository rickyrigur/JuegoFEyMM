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
    public Text nombreCamara;


    public void StartStopCamera()
    {        
        if(camTexture != null)
        {
            imgDisplay.texture = null;
            camTexture.Stop();
            camTexture = null;
            imgDisplay.material.mainTexture = null;

            textoCamOnOff.text = "Camera ON";
        }
        else
        {
            WebCamDevice device = WebCamTexture.devices[1];
            nombreCamara.text = "Camara: " + device.name;
            camTexture = new WebCamTexture(device.name);
            imgDisplay.texture = camTexture;
            imgDisplay.material.mainTexture = camTexture;

            camTexture.Play();

            textoCamOnOff.text = "Camera OFF";
        }
    }

}
