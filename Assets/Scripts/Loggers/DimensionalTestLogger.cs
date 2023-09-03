using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalTestLogger : TestLogger<DimensionalChangeTestSO, DimensionalChangeLevelSO>
{
    private int _correct;
    private int _incorrect;
    private int _testNumber;

    public override void LoadTest(DimensionalChangeTestSO test)
    {
        _testNumber++;
        logEvent.Happen("\n---- Nuevo Test ----\n");
        logEvent.Happen("Test Número: " + _testNumber + " - " + test.name + "\n");
        logEvent.Happen("Criterio: " + test.criteria + "\n");
    }

    public override void LoadLevel(DimensionalChangeLevelSO level)
    {
        _testNumber = 0;
        logEvent.Happen("\n-------------------------------------------\n");
        logEvent.Happen("Nivel: " + level.level + "\n");
    }

    public override void LogTest()
    {
        if (_correct != 0 || _incorrect != 0)
        {
            logEvent.Happen("Correctos: " + _correct + "\n");
            logEvent.Happen("Incorrectos: " + _incorrect + "\n");
            logEvent.Happen("Tiempo: " + _time + " Segundos \n");
        }

        ResetLogger();
    }

    public override void ReloadLevel()
    {
        if (_correct != 0 || _incorrect != 0)
        {
            string log = $"Correctos: {_correct} \nIncorrectos: {_incorrect} \nTiempo: {_time} Segundos \nRejugando nivel\n";
            logEvent.Happen(log);
        }
        _testNumber = 0;
        ResetLogger();
    }

    public override void ResetLogger()
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
