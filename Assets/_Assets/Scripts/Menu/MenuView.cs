using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Menu
{
    public sealed class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        public IObservable<Unit> StartClicked =>
            _startButton != null
                ? _startButton.OnClickAsObservable()
                : Observable.Never<Unit>();
    }
}
