using System;
using Cysharp.Threading.Tasks;
using Events;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tail
{
    public class AutoTailDrawer : MonoBehaviour
    {
        [Header("TailSettings")]
        [SerializeField] private TailUnit _tailPrefab;
        [SerializeField] private Transform _followTarget;
        [SerializeField] private TailEvent _tailCreatedEvent;
        
        [Header("Draw Settings")]
        [SerializeField] private float PointSpacing = 0.1f;
        [SerializeField] private Vector2 _drawLengthRange = Vector2.up;
        [SerializeField] private float _coolDownLength = 0.3f;

        private TailUnit _currentTail;
        private IDisposable _drawRegistration;
        private IDisposable _coolDownRegistration;
        public bool IsDrawing { get; private set; }
        
        private float _tailWidthScale = 1;
        
        public void StartDraw()
        {
            if (IsDrawing)
            {
                return;
            }
            
            IsDrawing = true;
            _currentTail = Instantiate(_tailPrefab,transform);
            _tailCreatedEvent.Invoke(_currentTail);
            _currentTail.ScaleWidth(_tailWidthScale);
            _currentTail.AddPoint(_followTarget.position);
            _drawRegistration = Observable.EveryFixedUpdate().Subscribe(_ => GenerateTailPoint());
            var drawLength = Random.Range(_drawLengthRange.x, _drawLengthRange.y);
            _coolDownRegistration =  Observable.EveryUpdate().Where((_ => _currentTail.Length >= drawLength)).Take(1).Subscribe(_ => CoolDown());
        }

        public async UniTask StopDraw()
        {
            if (IsDrawing == false)
            {
                return;
            }
            
            _drawRegistration.Dispose();
            _coolDownRegistration.Dispose();
            
            IsDrawing = false;
            await UniTask.WaitUntil(() => _currentTail == null ||
                Vector2.Distance(_currentTail.LastPoint(), _followTarget.position) > _coolDownLength);
            if (_currentTail == null)
            {
                return;
            }
            _currentTail.Detach();
        }

        private async void CoolDown()
        {
            print("Cool Downs");
            await StopDraw();
            StartDraw();
        }

        private void GenerateTailPoint()
        {
            if (Vector2.Distance(_currentTail.LastPoint(), _followTarget.position) > PointSpacing)
            {
                _currentTail.AddPoint(_followTarget.position);
            }
        }

        public void Scale(float scale)
        {
            StopDraw().Forget();
            PointSpacing *= scale;
            _coolDownLength *= scale;
            _tailWidthScale *= scale;
            StartDraw();
        }
    }
}