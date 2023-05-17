using UnityEngine;

public abstract class TestLogger<Test, Level> : MonoBehaviour
{
    public GameEventSO_S logEvent;
    protected float _time;

    private void Update()
    {
        _time += Time.deltaTime;
    }

    public abstract void LoadTest(Test test);

    public abstract void LoadLevel(Level level);

    public abstract void LogTest();

    public abstract void ResetLogger();
}
