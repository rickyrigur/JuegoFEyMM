using UnityEngine;
using UnityEngine.Windows.WebCam;
using System.Linq;
using System;
using UnityEngine.Events;

//using System; Incluyan esto para poder sacar de forma correcta la fecha cuando inicia a grabar

public class VideoCaptureExample : MonoBehaviour
{
    static readonly float MaxRecordingTime = 5.0f;

    VideoCapture m_VideoCapture = null;
    bool _stopped; //Usaria esta variable local para que puedan frenar los updates y que no se llame al stop multiples veces.
    float m_stopRecordingTimer = float.MaxValue; //No lo inicializaria en un valor tan alto. Se puede inicializar en cero

    string _path;
	
	public UnityEvent<string> onEndRecording;
	
    void Start()
    {
        StartVideoCaptureTest();
    }

    void Update()
    {
        if (m_VideoCapture == null || !m_VideoCapture.IsRecording || _stopped) //Agregaria aca la variable local stopped o algo , que se setearia al iniciar y dejar de grabar
        {
            return;
        }

        if (Time.time > m_stopRecordingTimer) 
        {
            _stopped = true;
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);            
            /*
             * Al ser operaciones asincronicas, se va a llamar constantemente al stop recording incluso aunque ya se haya
             * llamado una vez. Deberian de usar la variable local stopped para que no permita que se siga llamando al update
             * en caso de estar frenando la grabacion. Igual, dudo que en la version final el tiempo de grabacion se rija
             * por tiempo, siendo que una sesion tiene diferentes tiempos. Pueden agregarlo, eso si, como una condicion
             * extra para que los videos no sean muy pesados, siempre que vuelvan a iniciar la grabacion despues
            */
        }
    }

    void StartVideoCaptureTest()
    {
        /* Este metodo deberia ser publico, de forma que pueda llamarse desde otras partes del juego, desde un
         * Unity Event, o un event manager que creen propio.
        */
        Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        Debug.Log(cameraResolution);

        float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();
        Debug.Log(cameraFramerate);


        VideoCapture.CreateAsync(false, delegate (VideoCapture videoCapture)
        {
            if (videoCapture != null)
            {
                m_VideoCapture = videoCapture;
                Debug.Log("Created VideoCapture Instance!");

                CameraParameters cameraParameters = new CameraParameters();
                cameraParameters.hologramOpacity = 0.0f;
                cameraParameters.frameRate = cameraFramerate;
                cameraParameters.cameraResolutionWidth = cameraResolution.width;
                cameraParameters.cameraResolutionHeight = cameraResolution.height;
                cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

                m_VideoCapture.StartVideoModeAsync(cameraParameters,
                    VideoCapture.AudioState.ApplicationAndMicAudio,
                    OnStartedVideoCaptureMode);
            }
            else
            {
                Debug.LogError("Failed to create VideoCapture Instance!");
            }
        });
    }

    void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Video Capture Mode!");
        /* Time.time devuelve el tiempo desde el primer frame, no la fecha exacta. Quizas quieran cambiar 
         * esto por DateTime.Now (tienen que incluir la libreria de System en los includes)
         * Tambien, si usan esta opcion, deberian formatear el timestamp, ya que lo trae en fecha dd/mm/aaaa hh:mm:ss
         * como por ejemplo 23/02/2023 16:45:23 y puede generar un error de directorios por las /
         * Les recomiendo usar lo siguiente
         * string timeStamp = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("/", "");
         */
        string timeStamp = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("/", "");
        string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
        string filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);
        filepath = filepath.Replace("/", @"\");
        _path = filepath; //Lo guardo para pasarlo al uploader
        Debug.Log("PATH: " + filepath);
        m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
    }


    void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Recording Video!");
        //Aca deberian setear la variable local stopped, para que permita correr el update
        _stopped = false;
        m_stopRecordingTimer = Time.time + MaxRecordingTime;
    }

    void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        /* Este metodo deberia ser publico, de forma que pueda llamarse desde otras partes del juego, desde un
         * Unity Event, o un event manager que creen propio. Esto deberia tambien setear la variable local stopped que
         * frena el update.
        */
        Debug.Log("Stopped Recording Video!");
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }
    void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Video Capture Mode!");
		onEndRecording?.Invoke(_path);
        //FindObjectOfType<URLUploader>().UploadFile(_path); //Hago el upload del video, se puede hacer algo mas prolijo
    }
}
