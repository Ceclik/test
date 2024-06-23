using System;
using System.Collections.Generic;
using CharacterScripts;
using Unity.VisualScripting;
using UnityEngine;

public class QueueHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;

    [Space(10)][Header("Timings")]
    [SerializeField] private float startSpawnInterval;
    [SerializeField] private float deltaInterval;
    [SerializeField] private int peopleAmountToDecrease;
    
    private int _currentIndex;

    private float _timer;
    private float _currentInterval;

    private int _passedCharactersCounter;

    private void Start()
    {
        _currentInterval = startSpawnInterval;
        AddToQueue();
    }

    private void AddToQueue()
    {
        characters[_currentIndex].GetComponent<CharacterMover>().MoveUp = true;
        characters[_currentIndex].GetComponent<CharacterTimer>().IsInQueue = true;
        _currentIndex++;
        
        if (_currentIndex == 3)/////////////////////////////////////////////////////////TODO переделать
            _currentIndex = 0;

        _passedCharactersCounter++;
    }

    private void QuitQueue(CharacterTimer timer)
    {
        timer.QueueQuit();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _currentInterval)
        {
            _timer = 0;
            AddToQueue();
        }
        
        foreach (var character in characters)
        {
            CharacterTimer timer = character.GetComponent<CharacterTimer>();
            if (timer.Timer <= 0)
            {
                QuitQueue(timer);
            }
        }
    }
}
