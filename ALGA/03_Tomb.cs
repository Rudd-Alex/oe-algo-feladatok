using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
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
    public class TombLista<T> : Lista<T>, IEnumerable<T>
    {
        T[] E;
        int n;

        public TombLista(int meret = 5)
        {
            E = new T[meret];
            n = 0;
        }

        public int Elemszam => n;

        public void Bejar(Action<T> muvelet)
        {
            for (int i = 0; i < n; i++)
            {
                muvelet(E[i]);
            }
        }

        public void Beszur(int index, T ertek)
        {
            if (index < 0 || index > n)
                throw new HibasIndexKivetel();

            if (n == E.Length - 1)
                ExpandArray();

            for (int i = n; i > index; i--)
            {
                E[i] = E[i - 1];
            }
            E[index] = ertek;
            n++;
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n, ertek);
        }
        void ExpandArray()
        {
            T[] newE = E;
            E = new T[n * 2];
            for (int i = 0; i < n; i++)
            {
                E[i] = newE[i];
            }
        }

        public T Kiolvas(int index)
        {
            if (index < 0 || index >= n)
                throw new HibasIndexKivetel();

            return E[index];
        }

        public void Modosit(int index, T ertek)
        {
            if (index < 0 || index >= n)
                throw new HibasIndexKivetel();

            E[index] = ertek;
        }

        public void Torol(T ertek)
        {
            int db = 0;
            for (int i = 0; i < n; i++)
            {
                if (E[i].Equals(ertek))
                {
                    db++;
                }
                else
                {
                    E[i - db] = E[i];
                }
            }

            n = n - db;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TombListaBejaro<T>(E, n);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    class TombListaBejaro<T> : IEnumerator<T>
    {
        T[] E;
        int n;
        int aktualisIndex = -1;

        public TombListaBejaro(T[] E, int n)
        {
            this.E = E;
            this.n = n;
        }

        public T Current => E[aktualisIndex];

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            aktualisIndex++;
            return aktualisIndex < n;
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
