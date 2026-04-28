using System;
using UniRx;

namespace MicroFootball.Menu.Model
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
