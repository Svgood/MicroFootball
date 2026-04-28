using JetBrains.Annotations;
using Zenject;

namespace _Assets.Scripts.Common
{
    public class CustomFactory<TInterface, [MeansImplicitUse]TConcrete> : IFactory<TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create()
        {
            return _container.Instantiate<TConcrete>();
        }
    }

    public class CustomFactory<TA, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1});
        }
    }

    public class CustomFactory<TA, TB, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TB, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1, TB param2)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1, param2});
        }
    }

    public class CustomFactory<TA, TB, TC, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TB, TC, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1, TB param2, TC param3)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1, param2, param3});
        }
    }

    public class CustomFactory<TA, TB, TC, TD, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TB, TC, TD, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1, TB param2, TC param3, TD param4)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1, param2, param3, param4});
        }
    }

    public class CustomFactory<TA, TB, TC, TD, TE, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TB, TC, TD, TE, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1, TB param2, TC param3, TD param4, TE param5)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1, param2, param3, param4, param5});
        }
    }

    public class CustomFactory<TA, TB, TC, TD, TE, TF, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TB, TC, TD, TE, TF, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1, TB param2, TC param3, TD param4, TE param5, TF param6)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1, param2, param3, param4, param5, param6});
        }
    }
    
    public class CustomFactory<TA, TB, TC, TD, TE, TF, TG, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TB, TC, TD, TE, TF, TG, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1, TB param2, TC param3, TD param4, TE param5, TF param6, TG param7)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1, param2, param3, param4, param5, param6, param7});
        }
    }
    
    public class CustomFactory<TA, TB, TC, TD, TE, TF, TG, TH, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TB, TC, TD, TE, TF, TG, TH, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1, TB param2, TC param3, TD param4, TE param5, TF param6, TG param7, TH param8)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1, param2, param3, param4, param5, param6, param7, param8});
        }
    }
    
    public class CustomFactory<TA, TB, TC, TD, TE, TF, TG, TH, TI, TInterface, [MeansImplicitUse]TConcrete> : IFactory<TA, TB, TC, TD, TE, TF, TG, TH, TI, TInterface> where TConcrete : TInterface
    {
        [Inject] private readonly DiContainer _container;

        public TInterface Create(TA param1, TB param2, TC param3, TD param4, TE param5, TF param6, TG param7, TH param8, TI param9)
        {
            return _container.Instantiate<TConcrete>(new object[] {param1, param2, param3, param4, param5, param6, param7, param8, param9});
        }
    }
}