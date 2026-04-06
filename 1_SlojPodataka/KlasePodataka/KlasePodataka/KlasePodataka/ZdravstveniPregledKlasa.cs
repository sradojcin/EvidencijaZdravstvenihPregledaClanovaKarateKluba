using System;

namespace KlasePodataka
{
    public class ZdravstveniPregledKlasa
    {
        // atributi
        private int _zdravstveniPregledID;
        private int _clanID;
        private DateTime _datumPregleda;
        private string _napomena;

        // property
        public int ZdravstveniPregledID
        {
            get { return _zdravstveniPregledID; }
            set { _zdravstveniPregledID = value; }
        }

        public int ClanID
        {
            get { return _clanID; }
            set { _clanID = value; }
        }

        public DateTime DatumPregleda
        {
            get { return _datumPregleda; }
            set { _datumPregleda = value; }
        }

        public string Napomena
        {
            get { return _napomena; }
            set { _napomena = value; }
        }

        // konstruktor
        public ZdravstveniPregledKlasa()
        {

        }

        public ZdravstveniPregledKlasa(int zdravstveniPregledID, int clanID, DateTime datumPregleda, string napomena)
        {
            _zdravstveniPregledID = zdravstveniPregledID;
            _clanID = clanID;
            _datumPregleda = datumPregleda;
            _napomena = napomena;
        }
    }
}