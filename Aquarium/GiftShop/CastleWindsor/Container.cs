using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Aquarium.GiftShop.CastleWindsor
{
    public static class Container
    {
        private static IWindsorContainer _container;


        static Container()
        {
            _container = new WindsorContainer();
        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static void Install(IWindsorInstaller installer)
        {
            _container.Install(new[] { installer });
        }

        public static IWindsorContainer GetContainer()
        {
            return _container;
        }

        public static void Release(Object type)
        {
            _container.Release(type);
        }

        public static void Dispose()
        {
            _container.Dispose();
            _container = null;
        }
    }
}
