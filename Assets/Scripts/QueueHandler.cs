using CharacterScripts;
using System.Collections;
using TMPro;
using UnityEngine;

public class QueueHandler : MonoBehaviour
{
	[SerializeField] private GameObject[] characters;

	[Space(10)][Header("Timings")]
	[SerializeField] private float startSpawnInterval;
	[SerializeField] private float deltaInterval;
	[SerializeField] private int charactersAmountToDecrease;
	[SerializeField] private float minSpawnInterval;

	[Space(10)] [Header("Texsts")] [SerializeField]
	private TextMeshProUGUI queueCounterText; 
	
	private int _currentIndex;

	private float _timer;
	private float _currentInterval;

	private int _passedCharactersCounter;
	private int _queueCounter;
	private int _onScreenCounter;

	private void Start()
	{
		_currentInterval = startSpawnInterval;
		_queueCounter++;
		AddToScreen();
	}
	
	private void ManageQueueText(bool isInExit)
	{
		if(isInExit)
			Debug.Log($"queue counter in exit: {_queueCounter}\non screen counter: {_onScreenCounter}");
		else
			Debug.Log($"queue counter in add: {_queueCounter}\non screen counter: {_onScreenCounter}");
			
			
		if (_queueCounter > 3)
			queueCounterText.text = $"+{_queueCounter - 3}";
		if (_queueCounter <= 3) queueCounterText.text = " ";
	}
	
	private void AddToQueue()
	{
		_timer = 0;
		_queueCounter++;
		_passedCharactersCounter++;
		Debug.Log("Add to queue by timer");
	}
	

	private void AddToScreen()
	{
		if(_onScreenCounter < 3)
		{
			characters[_currentIndex].GetComponent<CharacterSpriteSetter>().SetSprite();
			characters[_currentIndex].GetComponent<CharacterMover>().MoveUp = true;
			characters[_currentIndex].GetComponent<CharacterTimer>().IsInQueue = true;
			_currentIndex++;
			_onScreenCounter++;
		
			if (_currentIndex == 3)
				_currentIndex = 0;
		}
		
		
		ManageQueueText(false);
	}

	public void QuitQueue(CharacterTimer timer)
	{
		timer.QueueQuit();
		_onScreenCounter--;
		
		ManageQueueText(true);

		if(_queueCounter > 3)
			StartCoroutine(AddToScreenDelayed());
			
		_queueCounter--;
	}

	private IEnumerator AddToScreenDelayed(){
		yield return new WaitForSeconds(1.5f);
		AddToScreen();
	}

	private void Update()
	{
		_timer += Time.deltaTime;

		if (_timer >= _currentInterval)
		{
			AddToQueue();
			
			AddToScreen();
		}
		
		foreach (var character in characters)
		{
			CharacterTimer timer = character.GetComponent<CharacterTimer>();
			if (timer.Timer <= 0 && timer.IsInQueue)
			{
				QuitQueue(timer);
				timer.IsInQueue = false;
			}
		}

		if (_passedCharactersCounter == charactersAmountToDecrease && _currentInterval > minSpawnInterval)
		{
			Debug.Log("Decreasing interval");
			_currentInterval -= deltaInterval;
			_passedCharactersCounter = 0;
		}
	}
}
