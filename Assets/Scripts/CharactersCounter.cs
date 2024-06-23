using System;
using UnityEngine;

public class CharactersCounter : MonoBehaviour
{
    [SerializeField] private int amountOfCharactersToDecrease;

    private int _counter;

    public delegate void decreaseTimers();

    public event decreaseTimers OnCounterComplete;

    public void AddCharacter()
    {
        _counter++;
    }

    private void Update()
    {
        if (_counter == 20)
        {
            _counter = 0;
            OnCounterComplete?.Invoke();
        }
    }
}
