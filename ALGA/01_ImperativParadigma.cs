using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    interface IVegrehajthato
    {
        public void Vegrehajtas();
    }
    class FeladatTarolo<T> where T : IVegrehajthato
    {
        T[] tarolo;
        int n;

        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
            n = 0;
        }
    }
}
