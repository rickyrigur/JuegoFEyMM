using UnityEngine;
using UnityEngine.Events;
using FFmpegUnityBind2;
using FFmpegUnityBind2.Internal;
using UnityEngine.UI;

class VideoCallbackHandler : FFmpegCallbacksHandlerBase
{
    [SerializeField]
    private UnityEvent<string> OnProccessEnd;
    
    private string _path;

    private float _durationMS;
    private float _progress;
    private Image _progressBar;
    private Documento _documentLogger;

    public VideoCallbackHandler(UnityEvent<string> onProccessEnd, string path, Image progressBar, Documento documentLogger)
    {
        OnProccessEnd = onProccessEnd;
        _path = path;
        _progressBar = progressBar;
        _documentLogger = documentLogger;
    }

    public override void OnStart(long executionId)
    {
        _documentLogger.AddToDocument("INICIO DE COMPILACION");
        base.OnStart(executionId);
    }

    public override void OnError(long executionId, string message)
    {
        _documentLogger.AddToDocument($"ERROR: {message}");
        base.OnError(executionId, message);
    }

    public override void OnSuccess(long executionId)
    {
        _documentLogger.AddToDocument("COMPILACION CORRECTA");
        base.OnSuccess(executionId);
    }

    public override void OnFail(long executionId)
    {
        _documentLogger.AddToDocument("FALLA EN LA COMPILACION");
        base.OnFail(executionId);
    }

    public override void OnCanceled(long executionId)
    {
        _documentLogger.AddToDocument("COMPILACION CANCELADA");
        base.OnCanceled(executionId);
    }

    public override void OnWarning(long executionId, string message)
    {
        _documentLogger.AddToDocument($"WARNING: {message}");
        base.OnWarning(executionId, message);
    }

    public override void OnLog(long executionId, string message)
    {
        FFmpegProgressParser.Parse(message, ref _durationMS, ref _progress);
        _progressBar.fillAmount = _progress;
        _documentLogger.AddToDocument($"LOG: {message}");
        base.OnLog(executionId, message);
    }

    public override void OnFinish(long executionId)
    {
        _documentLogger.AddToDocument("COMPILACION COMPLETA");
        OnProccessEnd?.Invoke(_path);
        _progress = 1;
        _progressBar.fillAmount = _progress;
        base.OnFinish(executionId);
    }
}