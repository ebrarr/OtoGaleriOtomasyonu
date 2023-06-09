using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_Otomasyon_ConsoleApp
{
    internal class Galeri
    {
        public List<Araba> Arabalar = new List<Araba>();
        public Galeri()
        {
            SahteVeriGir();
        }
        public int GaleridekiAracSayisi
        {
            get
            {
                return this.Arabalar.Where(a => a.Durum == DURUM.Galeride).ToList().Count;
            }
        }
        public int KiradakiAracSayisi
        {
            get
            {
                return this.Arabalar.Where(a => a.Durum == DURUM.Kirada).ToList().Count;
            }
        }
        public int ToplamAracSayisi
        {
            get
            {
                return this.Arabalar.Count;
            }
        }
        public void ArabaEkle(string plaka, string marka, float kiralamaBedeli, ARACTIPI aracTipi)
        {
            //Parametreden aldığımız bilgiler ile yeni bir araba listesi oluşturarak bu bilgileri Araba classına göndererek bilgileri kaydederiz.
            Araba a = new Araba(plaka, marka, kiralamaBedeli, aracTipi);
            this.Arabalar.Add(a);
        }
        public void SahteVeriGir()
        {
            // Oluşturduğumuz Arabaekle metodu ile manuel bilgi girerek sahte veri oluştururuz.

            ArabaEkle("34arb1223", "FIAT", 70, ARACTIPI.Hatchback);
            ArabaEkle("35arb3556", "KIA", 60, ARACTIPI.SUV);
            ArabaEkle("36ar1247", "OPEL", 50, ARACTIPI.Sedan);

        }
        public DURUM DurumGetir(string plaka)
        {
            // parametreden aldığımız plaka bilgisi ile aradığımız aracı buluyoruz. Eğer araç yoksa Empty döndürür.
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();
            if (a != null)
            {
                return a.Durum;
            }
            return DURUM.Empty;
        }
        public void ArabaKirala(string plaka, int sure)
        {
            //parametreden aldığımız plaka bilgisi ile araba listesinden aradığımız aracı buluyoruz. Araç kira durumuna geçtiği için arabanın durumunu DURUM.Kirada'ya çeviriyoruz.
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();
            if (a != null && a.Durum == DURUM.Galeride)
            {
                a.Durum = DURUM.Kirada;
                a.KiralamaSureleri.Add(sure);
            }
        }
        public void ArabaTeslimAl(string plaka)
        {
            // parametreden aldığımız plaka bilgisi ile aradığımız aracı buluyoruz.
            // Eğer böyle bir araç varsa bulduğumuz aracın durumununu aracı teslim alacağımız için galeride olarak güncellenir.
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();

            if (a != null)
            {
                if (a.Durum == DURUM.Galeride)
                {
                    throw new Exception("Zaten galeride");
                }

                a.Durum = DURUM.Galeride;
            }
            else
            {
                throw new Exception("Bu plakada bir araç yok.");
            }
        }
        public List<Araba> ArabaListesiGetir(DURUM durum)
        {
            // parametreden durum veri tipinde aldığımız veri ile otogaleride araç durumlarına göre listeleme gerçekleştiriyoruz.

            List<Araba> liste = this.Arabalar;
            if (durum == DURUM.Kirada || durum == DURUM.Galeride)
            {
                liste = this.Arabalar.Where(a => a.Durum == durum).ToList();
            }
            return liste;
        }
        public void KiralamaIptal(string plaka)
        {
            //parametreden aldığımız plaka bilgisi ile aradığımız aracı buluyoruz.
            // Eğer böyle bir araç var ise kiralamayı iptal edeceğimiz için durumu galeride olarak güncelleyip kiralama süresini düşüyoruz.

            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();

            if (a != null)
            {
                a.Durum = DURUM.Galeride;
                a.KiralamaSureleri.RemoveAt(a.KiralamaSureleri.Count - 1);
            }

        }
        public void ArabaSil(string plaka)
        {
            Araba a = this.Arabalar.Where(x => x.Plaka == plaka.ToUpper()).FirstOrDefault();

            if (a != null && a.Durum == DURUM.Galeride)
            {
                this.Arabalar.Remove(a);
            }
        }
    }
}
