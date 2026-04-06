using System;
using System.Collections.Generic;
using System.Data;

namespace KlasePodataka
{
    public class ClanListaKlasa
    {
        // atribut
        private List<ClanKlasa> _lista;

        // property
        public List<ClanKlasa> Lista
        {
            get { return _lista; }
            set { _lista = value; }
        }

        // konstruktor
        public ClanListaKlasa()
        {
            _lista = new List<ClanKlasa>();
        }

        // metoda za popunjavanje liste iz DataSet-a
        public void NapuniListu(DataSet ds)
        {
            _lista.Clear();

            foreach (DataRow red in ds.Tables[0].Rows)
            {
                ClanKlasa clan = new ClanKlasa();

                clan.ClanID = Convert.ToInt32(red["ClanID"]);
                clan.JMBG = red["JMBG"].ToString();
                clan.Ime = red["Ime"].ToString();
                clan.Prezime = red["Prezime"].ToString();
                clan.DatumRodjenja = Convert.ToDateTime(red["DatumRodjenja"]);

                _lista.Add(clan);
            }
        }
    }
}