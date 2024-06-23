using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterScripts
{
    public class CharacterTimer : MonoBehaviour
    {
        [SerializeField] private float startTimerValue;
        [SerializeField] private float deltaTimerValue;
        [SerializeField] private CharactersCounter counter;
        [SerializeField] private float displaceDelay;

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

            if (IsInQueue && Timer <= 0)
                QueueQuit();
        }

        private void UpdateTimerValue()
        {
            startTimerValue -= deltaTimerValue;
        }

        public void QueueQuit()
        {
            IsInQueue = false;
            Timer = startTimerValue;
            _characterMover.MoveDown = true;
            StartCoroutine(QueueQuitDelayed());
        }

        private IEnumerator QueueQuitDelayed()
        {
            yield return new WaitForSeconds(displaceDelay);
            OnQueueQuit?.Invoke();
        }

        private void OnDestroy()
        {
            counter.OnCounterComplete -= UpdateTimerValue;
        }
    }
}
