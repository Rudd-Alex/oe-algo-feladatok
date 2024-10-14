using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class SzotarElem<K, T>
    {
        public K kulcs;
        public T t;

        public SzotarElem(K kulcs, T t)
        {
            this.kulcs = kulcs;
            this.t = t;
        }
    }
    public class HasitoSzotarTulcsordulasiTerulettel<K, T> : Szotar<K, T>
    {
        SzotarElem<K, T>?[] E;
        Func<K, int> h;
        LancoltLista<SzotarElem<K, T>> U;

        public HasitoSzotarTulcsordulasiTerulettel(int meret, Func<K, int> hasitoFuggveny)
        {
            E = new SzotarElem<K, T>[meret];
            h = (K x) => mod(hasitoFuggveny(x), E.Length);
            U = new LancoltLista<SzotarElem<K, T>>();
        }
        public HasitoSzotarTulcsordulasiTerulettel(int meret) : this(meret, (K x) => x.GetHashCode()) { }
        private SzotarElem<K, T>? KulcsKeres(K kulcs)
        {
            int i = h(kulcs);
            SzotarElem<K,T>? keresett = E[i];
            if (keresett != null && keresett.kulcs.Equals(kulcs))
            {
                return keresett;    
            }
            SzotarElem<K, T>? seged = null;
            U.Bejar((x) => { if (x.kulcs.Equals(kulcs)) seged = x; });

            return seged;
        }
        public void Beir(K kulcs, T ertek)
        {
            SzotarElem<K, T>? meglevo = KulcsKeres(kulcs);
            if (meglevo != null)
            {
                meglevo.t = ertek;
                return;
            }

            SzotarElem<K, T> uj = new SzotarElem<K, T>(kulcs, ertek);
            int index = h(kulcs);
            if (E[index] == null)
                E[index] = uj;
            else
                U.Hozzafuz(uj);
        }
        public T Kiolvas(K kulcs)
        {
            SzotarElem<K, T>? keresett = KulcsKeres(kulcs);
            if (keresett != null)
                return keresett.t;
            else
                throw new HibasKulcsKivetel();
        }
        public void Torol(K kulcs)
        {
            int i = h(kulcs);
            if (E[i] != null && E[i].kulcs.Equals(kulcs))
            {
                E[i] = null;
                return;
            }

            SzotarElem<K, T>? keresett = null;  
            U.Bejar((x) => { if (x != null && x.kulcs.Equals(kulcs)) keresett = x; });
            if (keresett != null)
                U.Torol(keresett);
            else
                throw new HibasIndexKivetel();
        }
        int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
