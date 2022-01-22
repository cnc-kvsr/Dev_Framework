using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);  //Verilen unique key'e göre cache datasını getirmek için
        void Add(string key, object data,int cacheTime);   //cacheTime : cache'de datanın ne kadar duracağının süresi
        bool IsAdd(string key);  //data cache'de var mı? 
        void Remove(string key); // cache'den datayı sil
        void RemoveByPattern(string pattern);
        void Clear(); // cache'i tamamen sil
    }
}
