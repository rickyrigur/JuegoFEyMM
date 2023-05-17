using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryLogger : TestLogger<MemoryTestSO, MemoryLevelSO>
{
    private int _testNumber;
    public override void LoadTest(MemoryTestSO test)
    {
        _testNumber++;
        logEvent.Happen("\n---- Nuevo Test ----\n");
        logEvent.Happen("Test Número: " + _testNumber + " - " + test.name + "\n");
        logEvent.Happen("Cantidad de cajas: " + test.boxesAmount + "\n");
        logEvent.Happen("Cantidad de juguetes a encontrar: " + test.objectAmount + "\n");
        logEvent.Happen("Distribucion: " + test.position.ToString() + "\n");
        logEvent.Happen("Tiempo de distracción: " + test.delay + "\n");
    }

    public override void LoadLevel(MemoryLevelSO level)
    {
        _testNumber = 0;
        logEvent.Happen("\n-------------------------------------------\n");
        logEvent.Happen("Nivel: " + level.level + "\n");
    }

    public override void LogTest()
    {
        if (GameVars.CorrectAmount != 0 || GameVars.WrongAmount != 0)
        {
            logEvent.Happen("Correctos: " + GameVars.CorrectAmount + "\n");
            logEvent.Happen("Incorrectos: " + GameVars.WrongAmount + "\n");
            logEvent.Happen("Tiempo: " + _time + " Segundos \n");
        }

        ResetLogger();
    }

    public override void ResetLogger()
    {
        GameVars.CorrectAmount = 0;
        GameVars.WrongAmount = 0;
        _time = 0;
    }
}
