using System;
namespace KlasePodataka
{
    public class ClanKlasa
    {
        // atributi
        private int _clanID;
        private string _jmbg;
        private string _ime;
        private string _prezime;
        private DateTime _datumRodjenja;

        // property
        public int ClanID
        {
            get { return _clanID; }
            set { _clanID = value; }
        }

        public string JMBG
        {
            get { return _jmbg; }
            set { _jmbg = value; }
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

        public DateTime DatumRodjenja
        {
            get { return _datumRodjenja; }
            set { _datumRodjenja = value; }
        }

        // konstruktor
        public ClanKlasa()
        {

        }

        public ClanKlasa(int clanID, string jmbg, string ime, string prezime, DateTime datumRodjenja)
        {
            _clanID = clanID;
            _jmbg = jmbg;
            _ime = ime;
            _prezime = prezime;
            _datumRodjenja = datumRodjenja;
        }
    }
}