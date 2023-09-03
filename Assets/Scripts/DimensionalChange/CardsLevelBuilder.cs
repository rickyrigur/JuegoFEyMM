using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CardsLevelBuilder : MonoBehaviour, ILevelBuilder<DimensionalChangeLevelSO, DimensionalChangeTestSO>
{
    public Basket basketLeft;
    public Basket basketRight;

    public List<DimensionalChangeLevelSO> levels;

    public DimensionalTestLogger logger;

    public UnityEvent OnNoMoreLevels;
    public UnityEvent OnBuildLevel;
    public UnityEvent OnBuildTest;

    private DimensionalChangeLevelSO _currentLevel;
    private DimensionalChangeTestSO _currentTest;

    private Queue<GameObject> _cards = new Queue<GameObject>();
    private Queue<CardSO> _loadedCards = new Queue<CardSO>();
    private Queue<DimensionalChangeLevelSO> _levelsQueue = new Queue<DimensionalChangeLevelSO>();
    private Queue<DimensionalChangeTestSO> _tests = new Queue<DimensionalChangeTestSO>();


    private readonly Vector3 _leftBasketPosition = new Vector3(-8.5f, 0f, 1);
    private readonly Vector3 _rightBasketPosition = new Vector3(8.5f, 0f, 1);
    private readonly Vector3 _middlePosition = new Vector3(0, -13, 1);
    private readonly Vector3 _sampleScale = new Vector3(0.18f, 0.18f, 0);

    private void Awake()
    {
        EnqueueLevels();
    }

    private void EnqueueLevels()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            _levelsQueue.Enqueue(levels[i]);
        }
    }

    public void LoadNextLevel()
    {
        if (_levelsQueue.Count > 0)
        {
            _currentLevel = _levelsQueue.Dequeue();
            BuildLevel(_currentLevel);
        }
        else
        {
            DestroyCards();
            OnNoMoreLevels?.Invoke();
        }
    }

    public void BuildLevel(DimensionalChangeLevelSO level)
    {
        logger.LoadLevel(level);
        EnqueueTests();
        OnBuildLevel?.Invoke();

        LoadNextTest();
    }

    private void EnqueueTests()
    {
        for (int i = 0; i < _currentLevel.fixedTests.Count; i++)
        {
            _tests.Enqueue(_currentLevel.fixedTests[i]);
        }

        System.Random rnd = new System.Random();
        List<DimensionalChangeTestSO> tempList = new List<DimensionalChangeTestSO>();
        tempList.AddRange(_currentLevel.randomTest);
        tempList = tempList.OrderBy(test => rnd.Next()).ToList();

        for (int i = 0; i < tempList.Count; i++)
        {
            _tests.Enqueue(tempList[i]);
        }
    }

    public void LoadNextTest()
    {
        logger.LogTest();
        if (_tests.Count > 0)
        {
            _currentTest = _tests.Dequeue();
            BuildTest(_currentTest);
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void ReplayTest()
    {
        logger.ReloadLevel();
        BuildTest(_currentTest, true);
    }

    public void BuildTest(DimensionalChangeTestSO test, bool replay = false)
    {
        logger.LoadTest(test);
        DestroyCards();
        BuildSamples(_currentLevel.cardM1, _currentLevel.cardM2);
        LoadCards(_currentLevel.cards);
        if (!replay)
            test.audioClip.Play();
        basketLeft.LoadCriteria(test.criteria);
        basketRight.LoadCriteria(test.criteria);
        TraerSiguienteCarta();

        OnBuildTest?.Invoke();
    }

    private void BuildSamples(CardSO sampleLeft, CardSO sampleRight)
    {
        GameObject sampleLeftPrefab = sampleLeft.Clone(_leftBasketPosition, Quaternion.identity, _sampleScale);
        GameObject sampleRightPrefab = sampleRight.Clone(_rightBasketPosition, Quaternion.identity, _sampleScale);
        sampleLeftPrefab.tag = "Muestra";
        sampleRightPrefab.tag = "Muestra";
        sampleLeftPrefab.GetComponent<Collider2D>().enabled = false;
        sampleRightPrefab.GetComponent<Collider2D>().enabled = false;

        basketLeft.LoadSampleCard(sampleLeftPrefab.GetComponent<Carta>());
        basketRight.LoadSampleCard(sampleRightPrefab.GetComponent<Carta>());

        _cards.Enqueue(sampleLeftPrefab);
        _cards.Enqueue(sampleRightPrefab);
    }

    public void LoadCards(List<CardSO> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            _loadedCards.Enqueue(cards[i]);
        }
    }

    public void TraerSiguienteCarta()
    {
        if (_loadedCards.Count > 0)
        {
            CardSO currentCard = _loadedCards.Dequeue();
            GameObject card = Instantiate(currentCard.Prefab, _middlePosition, Quaternion.identity);
            _cards.Enqueue(card);
        }
        else
        {
            _currentTest.eventOnEnd.Happen();
        }
    }

    public void DestroyCards()
    {
        int cardsAmount = _cards.Count;
        for (var i = 0; i < cardsAmount; i++)
        {
            GameObject currentCard = _cards.Dequeue();
            Destroy(currentCard);
        }
        _cards.Clear();
    }
}
