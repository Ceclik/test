using System.Collections;
using TMPro;
using UnityEngine;

namespace CharacterScripts
{
	public class CharacterTimer : MonoBehaviour
	{
		[SerializeField] private float startTimerValue;
		[SerializeField] private float deltaTimerValue;
		[SerializeField] private CharactersCounter counter;
		[SerializeField] private float displaceDelay;

		[Space(10)] [SerializeField] private TextMeshProUGUI timerText;

		public bool IsInQueue { get; set; }
		public float Timer { get; private set; }
		
		private CharacterMover _characterMover;

		public delegate void DisplaceCharacters();
		public event DisplaceCharacters OnQueueQuit;

		private void Start()
		{
			_characterMover = GetComponent<CharacterMover>();
			counter.OnCounterComplete += UpdateTimerValue;
			Timer = startTimerValue;
		}

		private void Update()
		{
			if (IsInQueue) Timer -= Time.deltaTime;

			timerText.text = ((int)Timer).ToString();
			
			if(Timer < 10)
				timerText.color = Color.red;
		}

		private void UpdateTimerValue()
		{
			startTimerValue -= deltaTimerValue;
		}

		public void QueueQuit()
		{
			IsInQueue = false;
			_characterMover.MoveDown = true;
			
			StartCoroutine(QueueQuitDelayed());
		}

		private IEnumerator QueueQuitDelayed()
		{
			yield return new WaitForSeconds(displaceDelay);
			Timer = startTimerValue;
			timerText.color = Color.black;
			OnQueueQuit?.Invoke();
		}

		private void OnDestroy()
		{
			counter.OnCounterComplete -= UpdateTimerValue;
		}
	}
}
