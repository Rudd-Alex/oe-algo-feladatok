using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class LancElem<T>
    {
        public T tart;
        public LancElem<T>? kov;

        public LancElem(T tart, LancElem<T>? kov)
        {
            this.tart = tart;
            this.kov = kov;
        }
    }
    public class LancoltVerem<T> : Verem<T>
    {
        LancElem<T>? fej;

        public LancoltVerem()
        {
            fej = null;
        }

        public bool Ures => fej == null;

        public T Felso()
        {
            if (fej == null)
                throw new NincsElemKivetel();

            return fej.tart;
        }

        public void Verembe(T ertek)
        {
            LancElem<T> uj = new LancElem<T> (ertek, fej);
            fej = uj;
        }

        public T Verembol()
        {
            if (fej == null)
                throw new NincsElemKivetel();

            T ertek = fej.tart;
            fej = fej.kov;
            return ertek;
        }
    }
    public class LancoltSor<T> : Sor<T>
    {
        LancElem<T>? fej = null;
        LancElem<T>? vege = null;
        public bool Ures => fej == null && vege == null;

        public T Elso()
        {
            if (fej == null)
                throw new NincsElemKivetel();

            return fej.tart;
        }

        public void Sorba(T ertek)
        {
            LancElem <T> uj = new LancElem<T>(ertek, null);
            if (fej == null)
            {
                fej = uj;
            }
            else
            {
                vege.kov = uj;
            }
            vege = uj;
        }

        public T Sorbol()
        {
            if (fej == null)
                throw new NincsElemKivetel();
            if (fej == vege)
                vege = null;

            T ertek = fej.tart;
            fej = fej.kov;

            return ertek;
        }
    }
    public class LancoltLista<T> : Lista<T>, IEnumerable<T>
    {
        LancElem<T>? fej = null;
        int elemszam = 0;

        public int Elemszam { get => elemszam; }

        public void Bejar(Action<T> muvelet)
        {
            LancElem<T> masol = fej;
            while (masol != null)
            {
                muvelet(masol.tart);
                masol = masol.kov;
            }
        }

        public void Beszur(int index, T ertek)
        {
            if (index < 0 || index > elemszam)
                throw new HibasIndexKivetel();

            elemszam++;
            if (index == 0)
            {
                LancElem<T> uj = new LancElem<T>(ertek, fej);
                fej = uj;
                return;
            }

            LancElem<T> masol = fej;
            for (int i = 1; i < index; i++)
            {
                masol = masol.kov;
            }
            LancElem<T> p = masol.kov;
            masol.kov = new LancElem<T>(ertek, p);

        }


        public void Hozzafuz(T ertek)
        {
            Beszur(elemszam, ertek);
        }

        public T Kiolvas(int index)
        {
            if (index < 0 || index >= elemszam)
                throw new HibasIndexKivetel();

            LancElem<T> masol = fej;
            for (int i = 0; i < index; i++)
            {
                masol = masol.kov;
            }

            return masol.tart;
        }

        public void Modosit(int index, T ertek)
        {
            if (index < 0 || index >= elemszam)
                throw new HibasIndexKivetel();

            LancElem<T> masol = fej;
            for (int i = 0; i < index; i++)
            {
                masol = masol.kov;
            }

            masol.tart = ertek;
        }

        public void Torol(T ertek)
        {
            LancElem<T>? masol = fej;
            LancElem<T>? elozo = null;

            while (masol.kov != null)
            {
                if (masol.tart.Equals(ertek))
                {
                    if (masol == fej)
                    {
                        fej = fej.kov;
                    }
                    else
                    {
                        elozo.kov = masol.kov;
                    }
                    elemszam--;
                }

                elozo = masol;
                masol = masol.kov;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new LancoltListaBejaro<T>(fej);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    public class LancoltListaBejaro<T> : IEnumerator<T>
    {
        LancElem<T>? fej;
        LancElem<T>? aktualisElem;
        bool elsoElott;

        public LancoltListaBejaro(LancElem<T>? fej)
        {
            this.fej = fej;
            this.aktualisElem = null;
            elsoElott = true;
        }

        public T Current => aktualisElem.tart;

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (aktualisElem != null)
            {
                aktualisElem = aktualisElem.kov;
            }
            else if (elsoElott)
            {
                elsoElott = false;
                aktualisElem = fej;
            }

            return aktualisElem != null;
        }

        public void Reset()
        {
            aktualisElem = null;
            elsoElott = true;
        }
    }
}
