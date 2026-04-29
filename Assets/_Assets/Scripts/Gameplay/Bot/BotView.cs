using System;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unit = UniRx.Unit;

namespace _Assets.Scripts.Gameplay.Bot
{
    public sealed class BotView : MonoBehaviour
    {
        private Subject<Unit> _onCollisionEnter = new Subject<Unit>();
        
        [SerializeField] private Transform _targetTransform;
        
        public IObservable<Unit> OnCollisionEnter => _onCollisionEnter;

        public Vector3 Position 
        {
            set => _targetTransform.position = value;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            _onCollisionEnter.OnNext(Unit.Default);
        }
    }
}
