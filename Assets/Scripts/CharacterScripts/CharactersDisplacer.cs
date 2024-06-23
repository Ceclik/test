using System;
using UnityEngine;

namespace CharacterScripts
{
    public class CharactersDisplacer : MonoBehaviour
    {
        [SerializeField] private Transform[] characters;
        [SerializeField] private Transform upperPointsParent;
        [SerializeField] private Transform lowerPointsParent;

        private Transform[] _upperPoints;
        private Transform[] _lowerPoints;


        private void OnEnable()
        {
            foreach (var character in characters)
                character.GetComponent<CharacterTimer>().OnQueueQuit += ChangeTargetPoints;
        }

        private void Start()
        {
            _upperPoints = new Transform[upperPointsParent.childCount];
            for (int i = 0; i < _upperPoints.Length; i++)
                _upperPoints[i] = upperPointsParent.GetChild(i);

            _lowerPoints = new Transform[lowerPointsParent.childCount];
            for (int i = 0; i < _lowerPoints.Length; i++)
                _lowerPoints[i] = lowerPointsParent.GetChild(i);

            for (int i = 0; i < characters.Length; i++)
            {
                CharacterMover mover = characters[i].GetComponent<CharacterMover>();
                mover.UpperTargetPoint = _upperPoints[i];
                mover.LowerTargetPoint = _lowerPoints[i];
                mover.PointIndex = i;
            }
        }

        private void ChangeTargetPoints()
        {
            foreach (var character in characters)
            {
                CharacterMover mover = character.GetComponent<CharacterMover>();
                if (mover.PointIndex == 0) mover.PointIndex = 2;
                else mover.PointIndex--;

                mover.UpperTargetPoint = _upperPoints[mover.PointIndex];
                mover.LowerTargetPoint = _lowerPoints[mover.PointIndex];

                mover.MoveToPoint = true;
            }
        }

        private void OnDisable()
        {
            foreach (var character in characters)
                character.GetComponent<CharacterTimer>().OnQueueQuit -= ChangeTargetPoints;
        }
    }
}
