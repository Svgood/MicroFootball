using System;
using UniRx;
using Zenject;

namespace _Assets.Scripts.Menu
{
    public class MenuTrigger 
    {
        private Subject<Unit> _startRequested = new Subject<Unit>();
        
        public IObservable<Unit> StartRequested => _startRequested;

        public void RequestStart()
        {
            _startRequested.OnNext(Unit.Default);
        }
    }
}