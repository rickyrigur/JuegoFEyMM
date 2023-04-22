using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MemoryLevelBuilder : MonoBehaviour, ILevelBuilder<MemoryLevelSO, MemoryTestSO>
{
    [SerializeField]
    private MemoryBox _box;
    [SerializeField]
    private List<GameObject> _objects;

    [SerializeField]
    private UnityEvent OnBuildTest;

    public MemoryTestSO test;

    private void Start()
    {
        BuildTest(test);
    }

    public void BuildLevel(MemoryLevelSO test)
    {

    }


    public void BuildTest(MemoryTestSO test, bool replay = false)
    {
        IBoxCreator creator = GetBoxCreator(test.position);
        creator.CreateBoxes(test.boxesAmount, _box);

        List<MemoryObjects> objectsToUse = new List<MemoryObjects>();
        List<MemoryObjects> tempList = new List<MemoryObjects>();
        tempList.AddRange(test.objects);

        for (int i = 0; i < test.objectAmount; i++)
        {
            int index = Random.Range(0, tempList.Count - 1);
            //Debug.Log(index);
            //objectsToUse.Add(tempList[index]);
            //tempList.RemoveAt(index);
        }

        //creator.HideElements(objectsToUse);
        OnBuildTest?.Invoke();
    }


    private IBoxCreator GetBoxCreator(Positions position)
    {
        switch (position)
        {
            case Positions.DIAGONAL:
                return new DiagonalBoxCreator();
            case Positions.HORIZONTAL:
                return new HorizontalBoxCreator();
            case Positions.L:
                return new LBoxCreator();
            default:
                Debug.LogWarning("WRONG BUILD POSITION: " + position);
                return new HorizontalBoxCreator();
        }
    }

}
