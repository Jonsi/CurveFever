using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tail
{
    public class PlayerTailDrawer : MonoBehaviour
    {
        [Header("TailSettings")]
        [SerializeField] private TailUnit _tailPrefab;
        [SerializeField] private Transform _head;
        
        [Header("Draw Settings")]
        [SerializeField] private float PointSpacing = 0.1f;
        [SerializeField] private Vector2 _drawLengthRange = Vector2.up;
        [SerializeField] private float _coolDownLength = 0.3f;

        private TailUnit _currentTail;
        private IDisposable _drawRegistration;
        private IDisposable _tailReachMax;

        private void OnEnable()
        {
            StartDraw();
        }

        private void OnDisable()
        {
            StopDraw();
        }

        public void StartDraw()
        {
            _currentTail = Instantiate(_tailPrefab,transform);
            _currentTail.AddPoint(_head.position);
            _drawRegistration = Observable.EveryFixedUpdate().Subscribe(_ => GenerateTailPoint());
            var drawLength = Random.Range(_drawLengthRange.x, _drawLengthRange.y);
            _tailReachMax =  Observable.EveryUpdate().Where((_ => _currentTail.Length >= drawLength)).Take(1).Subscribe(_ => CoolDown());
        }

        public void StopDraw()
        {
            _drawRegistration.Dispose();
            _tailReachMax.Dispose();
        }

        private async void CoolDown()
        {
            await UniTask.WaitUntil(() => Vector2.Distance(_head.position, _currentTail.LastPoint()) >= _coolDownLength).SuppressCancellationThrow();
            _currentTail.Detach();
            StartDraw();
        }

        private void GenerateTailPoint()
        {
            if (Vector2.Distance(_currentTail.LastPoint(), _head.position) > PointSpacing)
            {
                _currentTail.AddPoint(_head.position);
            }
        }
    }
}