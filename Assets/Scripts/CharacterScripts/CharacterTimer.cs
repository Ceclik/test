using UnityEngine;

namespace CharacterScripts
{
    public class CharacterTimer : MonoBehaviour
    {
        [SerializeField] private float startTimerValue;
        [SerializeField] private float deltaTimerValue;
        [SerializeField] private CharactersCounter counter;

        [HideInInspector] public bool IsInQueue;

        public float Timer { get; private set; }

        private void Start()
        {
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
            Debug.LogError("Out of queue!");    
            /*TODO  вызвать метод опускания вниз*/
        }

        private void OnDestroy()
        {
            counter.OnCounterComplete -= UpdateTimerValue;
        }
    }
}
