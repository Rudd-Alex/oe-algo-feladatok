using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class FaElem<T> where T : IComparable<T>
    {
        public T tart;
        public FaElem<T>? bal;
        public FaElem<T>? jobb;

        public FaElem(T tart, FaElem<T>? bal, FaElem<T>? jobb)
        {
            this.tart = tart;
            this.bal = bal;
            this.jobb = jobb;
        }
    }
    public class FaHalmaz<T> : Halmaz<T> where T : IComparable<T>
    {
        FaElem<T>? gyoker;
        public void Bejar(Action<T> muvelet)
        {
            ReszfaBejarasPreOrder(gyoker, muvelet);
        }

        public void Beszur(T ertek)
        {
            gyoker = ReszfabaBeszur(gyoker, ertek);
        }

        public bool Eleme(T ertek)
        {
            return ReszfaEleme(gyoker, ertek);
        }

        public void Torol(T ertek)
        {
            ReszfabolTorol(gyoker, ertek);
        }
        static FaElem<T> ReszfabaBeszur(FaElem<T>? p, T ertek)
        {
            if (p == null)
                return new FaElem<T>(ertek, null, null);

            if (p.tart.CompareTo(ertek) > 0)
            {
                p.bal = ReszfabaBeszur(p.bal, ertek);
            }
            else if (p.tart.CompareTo(ertek) < 0)
            {
                p.jobb = ReszfabaBeszur(p.jobb, ertek);
            }
            return p;
        }
        static bool ReszfaEleme(FaElem<T>? p, T ertek)
        {
            if (p == null)
                return false;

            if (p.tart.CompareTo(ertek) > 0)
                return ReszfaEleme(p.bal, ertek);
            if (p.tart.CompareTo(ertek) < 0)
                return ReszfaEleme(p.jobb, ertek);

            return true;
        }
        static FaElem<T> ReszfabolTorol(FaElem<T>? p, T ertek)
        {
            if (p == null)
                throw new NincsElemKivetel();

            if (p.tart.CompareTo(ertek) < 0)
                p.jobb = ReszfabolTorol(p.jobb, ertek);
            else if (p.tart.CompareTo(ertek) > 0)
                p.bal = ReszfabolTorol(p.bal, ertek);
            else if (p.bal == null)
                p = p.jobb;
            else if (p.jobb == null)
                p = p.bal;
            else p.bal = KetGyerekesTorles(p, p.bal);

            return p;
        }
        void ReszfaBejarasPreOrder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p == null)
                return;

            muvelet(p.tart);
            ReszfaBejarasPreOrder(p.bal, muvelet);
            ReszfaBejarasPreOrder(p.jobb, muvelet);
        }
        void ReszfaBejarasInOrder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p == null)
                return;

            ReszfaBejarasPreOrder(p.bal, muvelet);
            muvelet(p.tart);
            ReszfaBejarasPreOrder(p.jobb, muvelet);
        }
        void ReszfaBejarasPostOrder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p == null)
                return;

            ReszfaBejarasPreOrder(p.bal, muvelet);
            ReszfaBejarasPreOrder(p.jobb, muvelet);
            muvelet(p.tart);
        }
        static FaElem<T> KetGyerekesTorles(FaElem<T> e, FaElem<T>? r)
        {
            while (r.jobb != null)
            {
                r = r.jobb;
            }
            e.tart = r.tart;
            r = r.bal;
            return r;
        }
    }
}
