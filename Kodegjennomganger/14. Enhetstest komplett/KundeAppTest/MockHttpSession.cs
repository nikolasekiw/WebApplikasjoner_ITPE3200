using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KundeAppTest
{
    /**
     * Denne her er klippet og limt fra nettet. Bare gjort noen små endringer.
     * Den implementerer en ISession, dvs. at vi kan bruke klassen MockHttpSession
     * til å sette sessions-nøkler / sessions-variabler og hente de ut. Så vi mocker
     * da selve sesionshåndteringen ved hjelp av denne klassen. 
    **/
    public class MockHttpSession : ISession
    {
        Dictionary<string, object> sessionStorage = new Dictionary<string, object>();

        public object this[string name]
        {
            get { return sessionStorage[name]; }
            set { sessionStorage[name] = value; }
        }

        void ISession.Set(string key, byte[] value)
        {
            sessionStorage[key] = value;
        }

        bool ISession.TryGetValue(string key, out byte[] value)
        {
            if (sessionStorage[key] != null)
            {
                value = Encoding.ASCII.GetBytes(sessionStorage[key].ToString());
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        // de underligggende metodene er ikke nødvendige for mocking 

        IEnumerable<string> ISession.Keys
        {
            get { throw new NotImplementedException(); }
        }

        string ISession.Id
        {
            get { throw new NotImplementedException(); }
        }

        bool ISession.IsAvailable
        {
            get { throw new NotImplementedException(); }
        }

        void ISession.Clear()
        {
            throw new NotImplementedException();
        }
        
        void ISession.Remove(string key)
        {
            throw new NotImplementedException();
        }

        Task ISession.CommitAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task ISession.LoadAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
