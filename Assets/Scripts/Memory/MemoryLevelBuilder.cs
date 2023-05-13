using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MemoryLevelBuilder : MonoBehaviour, ILevelBuilder<MemoryLevelSO, MemoryTestSO>
{
    [SerializeField]
    private MemoryBox _box;
    [SerializeField]
    private GameObject _parent;
    [SerializeField]
    private List<Toy> _objects;
    [SerializeField]
    private Cross _cross;
    [SerializeField]
    private List<MemoryLevelSO> _levels;

    [SerializeField]
    private Transform _hiddenTransform;
    [SerializeField]
    private Transform _shownTransform;

    [SerializeField]
    private UnityEvent OnBuildTest;
    [SerializeField]
    private UnityEvent OnNoMoreLevels;

    private BoxCreatorFactory _boxCreatorFactory;
    private ToyFactory _toyFactory;

    private Distractor _distractor => FindObjectOfType<Distractor>();
    private TutorialController _tutorialController => FindObjectOfType<TutorialController>(); 

    private Queue<MemoryLevelSO> _enqueuedLevels = new Queue<MemoryLevelSO>();
    private Queue<MemoryTestSO> _enqueuedTests = new Queue<MemoryTestSO>();

    private MemoryTestSO _currentTest;
    private MemoryLevelSO _currentLevel;

    private delegate void WaitCallback();

    private void Start()
    {
        _boxCreatorFactory = new BoxCreatorFactory(_box, _parent.transform);
        _toyFactory = new ToyFactory(_objects, _cross, _hiddenTransform, _shownTransform);
        EnqueueAllLevels();
    }

    private void EnqueueAllLevels()
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            _enqueuedLevels.Enqueue(_levels[i]);
        }
    }

    public void BuildLevel(MemoryLevelSO level)
    {
        _currentLevel = level;
        _currentLevel.BuildLevel();
        EnqueueTests(level);
        OnNextTest();
    }

    private void EnqueueTests(MemoryLevelSO level)
    {
        _enqueuedTests.Clear();
        for (int i = 0; i < level.tests.Count; i++)
        {
            _enqueuedTests.Enqueue(level.tests[i]);
        }
    }

    public void BuildTest(MemoryTestSO test, bool replay = false)
    {
        _currentTest = test;
        List<MemoryObjects> objectsToUse = new List<MemoryObjects>();
        List<MemoryObjects> tempList = new List<MemoryObjects>();
        tempList.AddRange(test.objects);

        for (int i = 0; i < test.objectAmount; i++)
        {
            int index = Random.Range(0, tempList.Count);
            objectsToUse.Add(tempList[index]);
            tempList.RemoveAt(index);
        }

        IValidator validator = new EasyValidator(objectsToUse, _tutorialController);
        _toyFactory.ClearToys();
        _toyFactory.ClearCrosses();
        _boxCreatorFactory.CreateBoxesAndToys(test, objectsToUse, validator, _toyFactory);

        float introLenght = 0;
        if (test.introAudio != null)
        {
            introLenght = test.introAudio.Lenght;
            test.introAudio.Play();
        }

        StartCoroutine(WaitForSeconds(introLenght + test.timeToMemorize, _toyFactory.AnimateAllToys));

        StartCoroutine(WaitForSeconds(introLenght + test.timeToMemorize + 0.5f, () => {
            if (test.audio != null)
            {
                _currentTest.audio.Play();
                StartCoroutine(WaitForSeconds(test.audio.Lenght, () => {
                    _distractor.Animate(test.delay);
                    OnBuildTest?.Invoke();
                }));
            }
            else
            {
                _distractor.Animate(test.delay);
                OnBuildTest?.Invoke();
            }
        }));
    }

    private IEnumerator WaitForSeconds(float seconds, WaitCallback callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }

    public void OnNextTest()
    {
        if (_enqueuedTests.Count <= 0)
        {
            OnNextLevel();
        }
        else
        {
            BuildTest(_enqueuedTests.Dequeue());
        }
    }

    public void OnNextLevel()
    {
        if (_enqueuedLevels.Count <= 0)
        {
            OnNoMoreLevels?.Invoke();
        }
        else
        {
            BuildLevel(_enqueuedLevels.Dequeue());
        }
    }

    public void ReplayTest()
    {
        _boxCreatorFactory.CleanBoxes();
        OnBuildTest?.Invoke();
    }
}
