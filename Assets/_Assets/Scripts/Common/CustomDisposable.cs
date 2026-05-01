using System;
using System.Threading;
using UniRx;

namespace _Assets.Scripts.Common
{
    public sealed class CustomDisposable : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private bool _disposed;

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        public void Add(IDisposable disposable)
        {
            _compositeDisposable.Add(disposable);
        }

        public void Clear()
        {
            _compositeDisposable.Clear();
        }

        public IDisposable OnDisposal(Action onDisposal)
        {
            var disposable = Disposable.Create(onDisposal);
            _compositeDisposable.Add(disposable);
            return disposable;
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            _cancellationTokenSource.Cancel();
            _compositeDisposable.Dispose();
            _cancellationTokenSource.Dispose();
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
