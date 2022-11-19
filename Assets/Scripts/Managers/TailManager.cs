using System;
using System.Collections.Generic;
using Events;
using Tail;
using UnityEngine;

namespace Managers
{
    public class TailManager : MonoBehaviour
    {
        public static TailManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] private TailEvent _tailCreatedEvent;
        [SerializeField] private List<TailUnit> _tails;
    
        private void OnEnable()
        {
            _tailCreatedEvent.RegisterListener(OnTailCreated);
        }

        private void OnTailCreated(TailUnit tail)
        {
            _tails.Add(tail);
        }

        public void RemoveAllTails()
        {
            print("removeing");
            foreach (var tail in _tails)
            {
                Destroy(tail.gameObject);
            }
        }
    }
}