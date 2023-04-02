using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelBuilder<T, R>
{
    void BuildLevel(T level);
    void BuildTest(R test, bool replay = false);
}
