using System;
using UniRx;

namespace _Assets.Scripts.Common
{
    public sealed class CustomDisposable : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Add(IDisposable disposable)
        {
            _compositeDisposable.Add(disposable);
        }

        public void Clear()
        {
            _compositeDisposable.Clear();
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }

    public static class CustomDisposableExtensions
    {
        public static TDisposable AddTo<TDisposable>(this TDisposable disposable, CustomDisposable customDisposable)
            where TDisposable : IDisposable
        {
            customDisposable.Add(disposable);
            return disposable;
        }
    }
}
