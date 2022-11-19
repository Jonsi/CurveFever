using System;
using Cysharp.Threading.Tasks;
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
        
        [Header("Draw Settings")]
        [SerializeField] private float PointSpacing = 0.1f;
        [SerializeField] private Vector2 _drawLengthRange = Vector2.up;
        [SerializeField] private float _coolDownLength = 0.3f;

        [Header("Collider Settings")]
        [SerializeField] private int colliderPointOffset = 1;

        private TailUnit _currentTail;
        private IDisposable _drawRegistration;
        private IDisposable _coolDownRegistration;
        private bool _isDrawing = false;
        private float _tailWidthScale = 1;
        
        public void StartDraw()
        {
            _isDrawing = true;
            _currentTail = Instantiate(_tailPrefab,transform);
            _currentTail.ScaleWidth(_tailWidthScale);
            _currentTail.AddPoint(_followTarget.position);
            _drawRegistration = Observable.EveryFixedUpdate().Subscribe(_ => GenerateTailPoint());
            var drawLength = Random.Range(_drawLengthRange.x, _drawLengthRange.y);
            _coolDownRegistration =  Observable.EveryUpdate().Where((_ => _currentTail.Length >= drawLength)).Take(1).Subscribe(_ => CoolDown());
        }

        public async UniTask StopDraw()
        {
            if (_isDrawing == false)
            {
                return;
            }
            
            _drawRegistration.Dispose();
            _coolDownRegistration.Dispose();
            
            _isDrawing = false;
            await UniTask.WaitUntil(() =>
                Vector2.Distance(_currentTail.LastPoint(), _followTarget.position) > _coolDownLength);
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

        public async void Scale(float scale)
        {
            StopDraw().Forget();
            PointSpacing *= scale;
            _coolDownLength *= scale;
            _tailWidthScale *= scale;
            StartDraw();
        }
    }
}