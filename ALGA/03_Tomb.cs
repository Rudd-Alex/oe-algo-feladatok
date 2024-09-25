using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class TombVerem<T> : Verem<T>
    {
        T[] E;
        int n;
        public bool Ures { get => n == 0; }

        public TombVerem(int meret)
        {
            E = new T[meret];
        }

        public T Felso()
        {
            if (n == 0)
                throw new NincsElemKivetel();

            return E[n - 1];
        }

        public void Verembe(T ertek)
        {
            if (n >= E.Length)
                throw new NincsHelyKivetel();

            E[n++] = ertek;
        }

        public T Verembol()
        {
            if (n == 0)
                throw new NincsElemKivetel();

            return E[--n];
        }
    }
    public class TombSor<T> : Sor<T>
    {
        T[] E;
        int n;
        int e;
        int u;
        public bool Ures { get => n == 0; }

        public TombSor(int meret)
        {
            E = new T[meret];
        }
        public T Elso()
        {
            if (n == 0)
                throw new NincsElemKivetel();

            return E[e];
        }

        public void Sorba(T ertek)
        {
            if (n >= E.Length)
                throw new NincsHelyKivetel();

            E[u] = ertek;
            u = (u + 1) % E.Length;
            n++;
        }

        public T Sorbol()
        {
            if (n == 0)
                throw new NincsElemKivetel();

            T ertek = E[e];
            e = (e + 1) % E.Length;
            n--;

            return ertek;
        }
    }
}
