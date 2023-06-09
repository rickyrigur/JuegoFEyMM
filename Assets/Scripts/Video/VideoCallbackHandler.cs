using UnityEngine;
using UnityEngine.Events;
using FFmpegUnityBind2;

class VideoCallbackHandler : FFmpegCallbacksHandlerBase
{
    [SerializeField]
    private UnityEvent<string> OnProccessEnd;
    private string _path;

    public VideoCallbackHandler(UnityEvent<string> onProccessEnd, string path)
    {
        OnProccessEnd = onProccessEnd;
        _path = path;
    }

    public override void OnFinish(long executionId)
    {
        OnProccessEnd?.Invoke(_path);
        base.OnFinish(executionId);
    }
}