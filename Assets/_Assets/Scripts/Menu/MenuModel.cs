using System;
using UniRx;

namespace _Assets.Scripts.Menu
{
    public sealed class MenuModel
    {
        private readonly Subject<Unit> _startRequested = new Subject<Unit>();

        public IObservable<Unit> StartRequested => _startRequested;

        public void RequestStart()
        {
            _startRequested.OnNext(Unit.Default);
        }
    }
}
