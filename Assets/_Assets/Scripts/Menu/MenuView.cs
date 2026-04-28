using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace MicroFootball.Menu.View
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
