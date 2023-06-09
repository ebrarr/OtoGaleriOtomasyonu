using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_Otomasyon_ConsoleApp
{
    internal class Uygulama
    {
        Galeri OtoGaleri = new Galeri();
        int sayac = 0;
        public void Calistir()
        {
            Menu();
            while (true)
            {
                string secim = SecimAl("Seçiminiz: ");
                Console.WriteLine();
                switch (secim)
                {
                    case "K":
                    case "1": ArabaKirala(); break;

                    case "T":
                    case "2": ArabaTeslimAl(); break;

                    case "R":
                    case "3": ArabalariListele(DURUM.Kirada); break;

                    case "M":
                    case "4": ArabalariListele(DURUM.Galeride); break;

                    case "A":
                    case "5": ArabalariListele(DURUM.Empty); break;

                    case "I":
                    case "6": KiralamaIptal(); break;

                    case "Y":
                    case "7": YeniArabaEkle(); break;

                    case "S":
                    case "8": ArabaSil(); break;

                    case "G":
                    case "9": BilgileriGoster(); break;

                    case "X":
                        // Seçim ekranında X'e basılması durumunda bir işlem yapılmadan tekrar seçim istenmesi adına burası boş bırakılabilir.
                        break;
                    case "ÇIKIŞ":
                        Cikis();
                        break;

                    default:
                        Console.WriteLine("Hatalı işlem gerçekleştirildi. Tekrar deneyin.");
                        sayac++;
                        break;



                }

            }

        }
        public void Menu()
        {
            Console.WriteLine("*** Galeri Otomasyonu ***");
            Console.WriteLine();
            Console.WriteLine("1- Araba Kirala (K)");
            Console.WriteLine("2- Araba Teslim Al (T)");
            Console.WriteLine("3- Kiradaki Arabaları Listele (R)");
            Console.WriteLine("4- Galerideki Arabaları Listele (M)");
            Console.WriteLine("5- Tüm Arabaları Listele (A)");
            Console.WriteLine("6- Kiralama İptali (I)");
            Console.WriteLine("7- Araba Ekle (Y)");
            Console.WriteLine("8- Araba Sil (S)");
            Console.WriteLine("9- Bilgileri Göster (G)");
            Console.WriteLine();
        }
        public string SecimAl(string mesaj)
        {
            Console.WriteLine();
            Console.Write(mesaj);
            if (sayac != 10)
            {
                return Console.ReadLine().ToUpper();
            }
            else
            {
                Console.WriteLine("Üzgünüm sizi anlayamıyorum. Program sonlandırılıyor.");
                return "ÇIKIŞ";
            }
        }
        public void ArabaKirala()
        {

            Console.WriteLine("*** Araba Kirala ***");
            Console.WriteLine();

            try
            {
                if (OtoGaleri.Arabalar.Count == 0)
                {
                    throw new Exception("Galeride araç bulunmamaktadır.");
                }
                else if (OtoGaleri.GaleridekiAracSayisi == 0)
                {
                    throw new Exception("Galerideki tüm araçlar kirada");
                }

                string plaka;
                while (true)
                {
                    //PlakaAl ve PlakaMi metotları ile plakanın doğruluğunu ve bu plakada bir araç olup olmadığını kontrol ediyoruz.
                    //SayiMi ile kullanıcının sayı girip girmediği kontrol ediliyor eğer sayı dışında bir karakter girmişse hata mesajı gösterip tekrar giriş sağlanıyor.
                    plaka = AracGerecler.PlakaAl("Kiralanacak arabanın plakasını giriniz: ");
                    if (plaka == "X") return;  // X olarak giriş yapıldığında metottan çıkmak için;

                    DURUM durum = OtoGaleri.DurumGetir(plaka);

                    if (durum == DURUM.Kirada)
                    {
                        Console.WriteLine("İstediğiniz araç şu anda kirada. Farklı araba seçiniz.");
                    }
                    else if (durum == DURUM.Empty)
                    {
                        Console.WriteLine("Galeriye ait bu plakada bir araç yok.");
                    }
                    else break;

                }

                int sure = AracGerecler.SayiAl("Kiralanma süresi: ");
                OtoGaleri.ArabaKirala(plaka, sure);
                Console.WriteLine();
                Console.WriteLine(plaka.ToUpper() + " plakalı araba " + sure + " saatliğine kiralandı.");

            }
            catch (Exception e)
            {
                if (e.Message == "Çıkış")
                {
                    return;
                }
                Console.WriteLine(e.Message);

            }


        }
        public void ArabaTeslimAl()
        {
            Console.WriteLine("*** Araba Teslim Al ***");
            Console.WriteLine();
            try
            {
                if (OtoGaleri.Arabalar.Count == 0)
                {
                    throw new Exception("Galeride hiç araba yok.");

                }
                else if (OtoGaleri.KiradakiAracSayisi == 0)
                {
                    throw new Exception("Kirada hiç araba yok.");

                }

                // Kullanıcıdan aldığımız plaka verisi ile arabanın olup olmadığı kirada mı değil mi kontrol yapılıp eğer teslimata uygun değilse hata döndürüp tekrar veri istenir.
                // aldığımız veriyi oluşturduğumuz galerideki teslim alma metoduna göndererek işlemi tamamlıyoruz.
                string plaka;

                while (true)
                {
                    plaka = AracGerecler.PlakaAl("Teslim edilecek arabanın plakası: ");

                    // X olarak giriş yapıldığında metottan çıkmak için;
                    if (plaka == "X")
                    {
                        return;
                    }
                    DURUM durum = OtoGaleri.DurumGetir(plaka);
                    if (durum == DURUM.Galeride)
                    {
                        Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                    }
                    else if (durum == DURUM.Empty)
                    {
                        Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    }
                    else break;

                }
                OtoGaleri.ArabaTeslimAl(plaka);
                Console.WriteLine();
                Console.WriteLine("Araba galeride beklemeye alındı.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ArabalariListele(DURUM durum)
        {
            Console.WriteLine(durum == DURUM.Kirada ? "*** Kiradaki Arabalar ***" : durum == DURUM.Galeride ? "*** Galerideki Arabalar ***" : "*** Tüm Arabalar ***");
            Console.WriteLine();
            ArabaListele(OtoGaleri.ArabaListesiGetir(durum));
        }
        public void ArabaListele(List<Araba> liste)
        {
            if (liste.Count == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
            Console.WriteLine("Plaka".PadRight(14) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araba Tipi".PadRight(12) +
                    "K. Sayısı".PadRight(12) + "Durum");
            Console.WriteLine("".PadRight(70, '-'));

            foreach (Araba item in liste)
            {
                Console.WriteLine(item.Plaka.PadRight(14) + item.Marka.PadRight(12) + item.KiralamaBedeli.ToString().PadRight(12) + item.AracTipi.ToString().PadRight(12) + item.KiralamaSayisi.ToString().PadRight(12) + item.Durum);
            }

        }
        public void KiralamaIptal()
        {

            Console.WriteLine("*** Kiralama İptali ***");
            Console.WriteLine();
            try
            {

                if (OtoGaleri.KiradakiAracSayisi == 0)
                {
                    throw new Exception("Kirada araba yok.");  // Hiç araba kiralanmadıysa bu hatayı döndürecek.

                }

                string plaka;

                while (true)
                {
                    plaka = AracGerecler.PlakaAl("Kiralaması iptal edilecek arabanın plakası: "); //Plaka Al metodu ile plakanın doğru girilip girilmediği kontrol edilerek DurumGetir metodu ile plakanın galerideki durumu sorgulanır.

                    // X olarak giriş yapıldığında metottan çıkmak için;
                    if (plaka == "X")
                    {
                        return;
                    }

                    DURUM durum = OtoGaleri.DurumGetir(plaka);
                    if (durum == DURUM.Galeride)
                    {
                        Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                    }
                    else if (durum == DURUM.Empty)
                    {
                        Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    }
                    else
                    {
                        break;
                    }
                }

                OtoGaleri.KiralamaIptal(plaka); // Kiralamaiptal metodu na kullanıcıdan aldığımız veri gönderek kiralamayı iptal ediyoruz.
                Console.WriteLine();
                Console.WriteLine("İptal gerçekleştirildi.");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
        public void YeniArabaEkle()
        {

            Console.WriteLine("*** Araba Ekle ***");
            Console.WriteLine();
            try
            {
                string plaka;
                while (true)
                {
                    plaka = AracGerecler.PlakaAl("Plaka: ");

                    // X olarak giriş yapıldığında metottan çıkmak için;
                    if (plaka == "X")
                    {
                        return;
                    }

                    // Araçgereçlerde oluştuğumuz plaka alma metodu ile  kullanıcıdan girdiği veri kontrol edilip hata varsa hata döndürülüp tekrar giriş istenilir.

                    if (OtoGaleri.DurumGetir(plaka) == DURUM.Kirada || OtoGaleri.DurumGetir(plaka) == DURUM.Galeride) // Eğer koşul sağlanıyoruz galeride böyle bir araba kayıtlıdır tekrar bilgi alınır.
                    {
                        Console.WriteLine("Aynı plakada araba mevcut. Girdiğiniz plakayı kontrol edin.");
                    }
                    else break;

                }
                // Araçgereçlere tanımladığımız ve hazırladığımız metodlar ile kulannıcıdan veri alarak ekle metoduna gönderiyoruz ve yeni araba kaydı yapılmış oluyor.
                string marka = AracGerecler.YaziAl("Marka: ");
                // X olarak giriş yapıldığında metottan çıkmak için;
                if (marka == "X")
                {
                    return;
                }

                float kiralamaBedeli = AracGerecler.SayiAl("Kiralama bedeli: ");
                ARACTIPI aracTipi = AracGerecler.AracTipiAl();
                OtoGaleri.ArabaEkle(plaka, marka, kiralamaBedeli, aracTipi);
                Console.WriteLine();
                Console.WriteLine("Araba başarılı bir şekilde eklendi.");
            }
            catch (Exception e)
            {
                if (e.Message == "Çıkış")
                {
                    return;
                }
                Console.WriteLine(e.Message);
            }

        }
        public void ArabaSil()
        {
            Console.WriteLine("*** Araba Sil ***");
            Console.WriteLine();
            string plaka;
            try
            {

                if (OtoGaleri.Arabalar.Count == 0)
                {
                    throw new Exception("Galeride silinecek araba yok.");
                }

                while (true)
                {
                    plaka = AracGerecler.PlakaAl("Silmek istediğiniz arabanın plakasını giriniz: ");

                    // X olarak giriş yapıldığında metottan çıkmak için;
                    if (plaka == "X")
                    {
                        return;
                    }

                    if (OtoGaleri.DurumGetir(plaka) == DURUM.Empty)
                    {
                        Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    }
                    else if (OtoGaleri.DurumGetir(plaka) == DURUM.Kirada)
                    {
                        Console.WriteLine("Araba kirada olduğu için silme işlemi gerçekleştirilemedi.");
                    }
                    else
                    {
                        break;
                    }
                }
                OtoGaleri.ArabaSil(plaka);
                Console.WriteLine();
                Console.WriteLine("Araba silindi.");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
        public void BilgileriGoster()
        {

            Console.WriteLine("*** Galeri Bilgileri ***");
            Console.WriteLine("Toplam araba sayısı: " + OtoGaleri.ToplamAracSayisi);
            Console.WriteLine("Kiradaki araba sayısı: " + OtoGaleri.KiradakiAracSayisi);
            Console.WriteLine("Bekleyen araba sayısı: " + OtoGaleri.GaleridekiAracSayisi);

        }
        public void Cikis()
        {
            Environment.Exit(0);
        }
    }
}
