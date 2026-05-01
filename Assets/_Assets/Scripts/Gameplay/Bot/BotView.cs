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
        private Subject<Collision> _onCollisionEnter = new Subject<Collision>();
        
        [SerializeField] private Transform _targetTransform;
        
        public IObservable<Collision> OnCollision => _onCollisionEnter;

        public Vector3 Position 
        {
            set => _targetTransform.position = value;
        }

        private void OnCollisionEnter(Collision other)
        {
            _onCollisionEnter.OnNext(other);
        }
    }
}
