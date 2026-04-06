using System;

namespace KlasePodataka
{
    public class KorisnikKlasa
    {
        // atributi
        private int _korisnikID;
        private string _korisnickoIme;
        private string _lozinka;
        private string _ime;
        private string _prezime;

        // property
        public int KorisnikID
        {
            get { return _korisnikID; }
            set { _korisnikID = value; }
        }

        public string KorisnickoIme
        {
            get { return _korisnickoIme; }
            set { _korisnickoIme = value; }
        }

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        // konstruktor
        public KorisnikKlasa()
        {

        }

        public KorisnikKlasa(int korisnikID, string korisnickoIme, string lozinka, string ime, string prezime)
        {
            _korisnikID = korisnikID;
            _korisnickoIme = korisnickoIme;
            _lozinka = lozinka;
            _ime = ime;
            _prezime = prezime;
        }
    }
}