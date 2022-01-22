using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public abstract class NHibernateHelper : IDisposable      //Farklı veritabanlarının konfigurasyonu için NHibernateHelper yazılır.
    {        
        private static ISessionFactory _sessionFactory;       //EntityFramework'deki context mantığı
        public virtual ISessionFactory SessionFactory         //Session'ı çözmek için..  
        {
            get { return _sessionFactory ?? (_sessionFactory = InitializeFactory()); }  // _sessionFactory'i döndür veya _sessionFactory null ise kendin bir tane Initialize et
        }

        protected abstract ISessionFactory InitializeFactory();    // ()Oracle, SQL vs..) Veritabanı implementasyonu  
                                                                   // Dependency Injection gibi... 
                                                                   // Implemente eden ortama göre değişiklik gösterebilmesi için abstract

        public virtual ISession OpenSession()  // Gönderilen SessionFactory'i kullanarak Session Aç
        {
            return SessionFactory.OpenSession();
        }

        public void Dispose() 
        {
            GC.SuppressFinalize(this);
        }
    }
}
