using UnityEngine;

public class TestLogger : MonoBehaviour
{
    public GameEventSO_S logEvent;

    private DimensionalChangeLevelSO _currentLevel;
    private DimensionalChangeTestSO _currentTest;

    private int _correct;
    private int _incorrect;
    private int _testNumber;
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;
    }

    public void LoadTest(DimensionalChangeTestSO test)
    {
        _currentTest = test;
        _testNumber ++;
        logEvent.Happen("\n---- Nuevo Test ----\n");
        logEvent.Happen("Test Número: " + _testNumber + " - " + _currentTest.name + "\n");
        logEvent.Happen("Criterio: " + _currentTest.criteria + "\n");
    }

    public void LoadLevel(DimensionalChangeLevelSO level)
    {
        _testNumber = 0;
        _currentLevel = level;
        logEvent.Happen("\n-------------------------------------------\n");
        logEvent.Happen("Nivel: " + _currentLevel.level + "\n");
    }

    public void LogTest()
    {
        if (_correct != 0 || _incorrect != 0)
        {
            logEvent.Happen("Correctos: " + _correct + "\n");
            logEvent.Happen("Incorrectos: " + _incorrect + "\n");
            logEvent.Happen("Tiempo: " + _time + " Segundos \n");
        }

        ResetLogger();
    }

    private void ResetLogger()
    {
        _correct = 0;
        _incorrect = 0;
        _time = 0;
    }

    public void Correct()
    {
        _correct++;
    }

    public void Incorrect()
    {
        _incorrect++;
    }

}
