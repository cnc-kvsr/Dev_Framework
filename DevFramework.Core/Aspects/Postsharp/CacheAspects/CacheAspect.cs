using DevFramework.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.Postsharp.CacheAspects
{
    [Serializable]
    public class CacheAspect:MethodInterceptionAspect
    {
        private Type _cacheType;
        private int _cacheByMinute;
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheByMinute=60)
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
        }

        //Gönderdiğimiz CacheManager'ı initialize etmemiz (yani instance'ını üretmemiz) gerekiyor.
        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType)==false) //gönderilen _cacheType CacheManager türünde değilse??
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType); 

            base.RuntimeInitialize(method);
        }

        //Method çalıştırılmadan önce datayı cache'e kaydetme
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}",  // methodun -> "{namespace},{class isim},{metod isim}"
                args.Method.ReflectedType.Namespace,  
                args.Method.ReflectedType.Name,
                args.Method.Name);

            var arguments = args.Arguments.ToList(); //metod parametreleri

            var key = string.Format("{0}({1})", methodName,
                string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));  //unique key

            if (_cacheManager.IsAdd(key))  //metod cache'de var mı
            {
                args.ReturnValue = _cacheManager.Get<object>(key); //datayı getir
            }
            base.OnInvoke(args);
            _cacheManager.Add(key, args.ReturnValue, _cacheByMinute);
        }
    }
}
