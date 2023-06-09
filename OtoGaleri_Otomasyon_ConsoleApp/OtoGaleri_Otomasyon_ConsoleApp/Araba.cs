using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_Otomasyon_ConsoleApp
{
    internal class Araba
    {

        public string Plaka;
        public string Marka;
        public float KiralamaBedeli;
        public ARACTIPI AracTipi;
        public DURUM Durum;
        public List<int> KiralamaSureleri = new List<int>();
        public int KiralamaSayisi
        {
            get
            {
                return this.KiralamaSureleri.Count;
            }
        }
        public Araba(string plaka, string marka, float kiralamaBedeli, ARACTIPI aracTipi) //parametreli kurucu metot ile listeye araba ekliyoruz.
        {
            this.Plaka = plaka.ToUpper();
            this.Marka = marka.ToUpper();
            this.KiralamaBedeli = kiralamaBedeli;
            this.AracTipi = aracTipi;
            this.Durum = DURUM.Galeride;
        }
    }
    enum ARACTIPI
    {
        Empty, Hatchback, Sedan, SUV
    }
    enum DURUM
    {
        Empty, Galeride, Kirada
    }

}
