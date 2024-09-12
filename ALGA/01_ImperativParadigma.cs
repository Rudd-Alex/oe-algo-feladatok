using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajthato
    {
        public void Vegrehajtas();
    }
    public class FeladatTarolo<T> : IEnumerable<T> where T : IVegrehajthato
    {
        protected T[] tarolo;
        protected int n;

        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
            n = 0;
        }
        public void Felvesz(T elem)
        {
            if (n == tarolo.Length)
            {
                throw new TaroloMegteltKivetel();
            }
            else
            {
                tarolo[n++] = elem;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FeladatTaroloBejaro<T>(tarolo, tarolo.Length);
        }

        public void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                tarolo[i].Vegrehajtas();
            }
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    public class FeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisIndex = -1;

        public FeladatTaroloBejaro(T[] tarolo, int n)
        {
            this.tarolo = tarolo;
            this.n = n;
        }

        public T Current => tarolo[aktualisIndex];

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
    public class FuggoFeladatTarolo<T> : FeladatTarolo<T> where T : IFuggo, IVegrehajthato
    {
        public FuggoFeladatTarolo(int meret) : base(meret)
        {
        }

        new public void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                if (tarolo[i].FuggosegTeljesul)
                    tarolo[i].Vegrehajtas();
            }
        }
    }

    public interface IFuggo
    {
        bool FuggosegTeljesul { get; }
    }
    public class TaroloMegteltKivetel : Exception
    {

    }
}
