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

        private TailUnit _currentTail;
        private IDisposable _drawRegistration;
        private IDisposable _tailReachMax;
        private bool _isDrawing = false;

        public void StartDraw()
        {
            _isDrawing = true;
            _currentTail = Instantiate(_tailPrefab,transform);
            _currentTail.AddPoint(_followTarget.position);
            _drawRegistration = Observable.EveryFixedUpdate().Subscribe(_ => GenerateTailPoint());
            var drawLength = Random.Range(_drawLengthRange.x, _drawLengthRange.y);
            _tailReachMax =  Observable.EveryUpdate().Where((_ => _currentTail.Length >= drawLength)).Take(1).Subscribe(_ => CoolDown());
        }

        public async void StopDraw()
        {
            if (_isDrawing == false)
            {
                return;
            }
            
            _drawRegistration.Dispose();
            _tailReachMax.Dispose();
            await UniTask.WaitUntil(() =>
                Vector2.Distance(_currentTail.LastPoint(), _followTarget.position) > PointSpacing);
            _currentTail.Detach();
        }

        private async void CoolDown()
        {
            print("Cool Downs");
            StopDraw();
            var isCancelled =await UniTask.WaitUntil(() => Vector2.Distance(_followTarget.position, _currentTail.LastPoint()) >= _coolDownLength).SuppressCancellationThrow();
            /*if (isCancelled)
            {
                print("canceled");//TODO: what?
                return;
            }*/
            StartDraw();
        }

        private void GenerateTailPoint()
        {
            if (Vector2.Distance(_currentTail.LastPoint(), _followTarget.position) > PointSpacing)
            {
                _currentTail.AddPoint(_followTarget.position);
            }
        }
    }
}