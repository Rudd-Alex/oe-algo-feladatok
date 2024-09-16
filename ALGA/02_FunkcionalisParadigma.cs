using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato
    {
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {
        }
        public Predicate<T> BejaroFeltetel { get; set; }
        public void FeltetelesVegrehajtas(Predicate<T> feltetel)
        {
            for (int i = 0; i < n; i++)
            {
                if (feltetel(tarolo[i]))
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            Predicate<T> feltetel = (BejaroFeltetel == null ? (T t) => true : BejaroFeltetel);
            return new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, feltetel);
        }
    }
    public class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisIndex = -1;
        Predicate<T> feltetel = null;
        public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Predicate<T> feltetel)
        {
            this.tarolo = tarolo;
            this.n = n;
            this.feltetel = feltetel;
        }

        public T Current => tarolo[aktualisIndex];

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            do
            {
                aktualisIndex++;
            } while ( aktualisIndex < n && !feltetel(tarolo[aktualisIndex]));

            return aktualisIndex < n;
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
