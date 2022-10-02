using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _goalName, _goalDescription;
    [SerializeField] private List<TMP_Text> _goalsInQueue;
    [SerializeField] private Color _textColor = Color.white;
    [SerializeField] private Color _seethroughColor = new Color(1f, 1f, 1f, 0.66f);
    [SerializeField] private Transform _queueParent;
    [SerializeField] private TMP_Text _textQueueEntryPrefab;

    private void OnEnable()
    {
        GameManager.OnNewGoal += UpdateUIOnGoalChange;
    }
    private void OnDisable()
    {
        GameManager.OnNewGoal -= UpdateUIOnGoalChange;
    }

    private void Awake()
    {
        AddGoalsInQueue();
    }

    private void Update()
    {
        _goalName.transform.localScale = Vector3.Lerp(_goalName.transform.localScale, Vector3.one, 6f * Time.deltaTime);
        _goalDescription.transform.localScale = Vector3.Lerp(_goalName.transform.localScale, Vector3.one, 6f * Time.deltaTime);
        _goalName.color = Color.Lerp(_goalName.color, _textColor, 6f * Time.deltaTime);
        _goalDescription.color = Color.Lerp(_goalDescription.color, _textColor, 6f * Time.deltaTime);
        for (int i = 0; i < _goalsInQueue.Count; i++)
        {
            Color col = _textColor;
            col.a = Mathf.Pow(0.5f, i);
            Vector3 scale = i == 0 ? Vector3.one * 1.3f : Vector3.one * 1.0f;
            _goalsInQueue[i].transform.localScale = Vector3.Lerp(_goalsInQueue[i].transform.localScale, scale, 6.0f * Time.deltaTime);
            _goalsInQueue[i].color = Color.Lerp(_goalsInQueue[i].color, col, 6.0f * Time.deltaTime);
        }
    }

    void AddGoalsInQueue()
    {
        foreach (var goal in GameManager.GetManager().goals)
        {
            var entry = Instantiate(_textQueueEntryPrefab);
            entry.text = goal._goalName;
            entry.transform.parent = _queueParent;
            _goalsInQueue.Add(entry);
        }
    }

    private void UpdateUIOnGoalChange()
    {
        if (_goalsInQueue.Count > 0)
        {
            Destroy(_goalsInQueue[0].gameObject);
            _goalsInQueue.RemoveAt(0);
        }
        UpdateGoalName();
        UpdateGoalDescription();
    }

    private void UpdateGoalName()
    {
        _goalName.text = GameManager.GetCurrentGoalName();
        _goalName.transform.localScale = Vector3.one * 2f;
        _goalName.color = GameManager.GetManager()._currentGoal._entryColor;
    }
    private void UpdateGoalDescription()
    {
        _goalDescription.text = GameManager.GetCurrentGoalDescription();
        _goalDescription.transform.localScale = Vector3.one * 2f;
        _goalName.color = GameManager.GetManager()._currentGoal._entryColor;
    }
}
