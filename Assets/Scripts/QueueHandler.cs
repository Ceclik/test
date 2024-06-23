using CharacterScripts;
using UnityEngine;

public class QueueHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;

    [Space(10)][Header("Timings")]
    [SerializeField] private float startSpawnInterval;
    [SerializeField] private float deltaInterval;
    [SerializeField] private int peopleAmountToDecrease;

    private void Update()
    {
        foreach (var character in characters)
        {
            CharacterTimer timer = character.GetComponent<CharacterTimer>();
            if (timer.Timer <= 0)
            {
                timer.QueueQuit();
                character.GetComponent<CharacterMover>().MoveCharacterDown();
            }
        }
    }
}
