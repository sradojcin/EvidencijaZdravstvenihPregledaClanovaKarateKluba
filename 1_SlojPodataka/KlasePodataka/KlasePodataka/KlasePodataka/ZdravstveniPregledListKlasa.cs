using System;
using System.Collections.Generic;
using System.Data;

namespace KlasePodataka
{
    public class ZdravstveniPregledListaKlasa
    {
        // atribut
        private List<ZdravstveniPregledKlasa> _lista;

        // property
        public List<ZdravstveniPregledKlasa> Lista
        {
            get { return _lista; }
            set { _lista = value; }
        }

        // konstruktor
        public ZdravstveniPregledListaKlasa()
        {
            _lista = new List<ZdravstveniPregledKlasa>();
        }

        // metoda za popunjavanje liste iz DataSet-a
        public void NapuniListu(DataSet ds)
        {
            _lista.Clear();

            foreach (DataRow red in ds.Tables[0].Rows)
            {
                ZdravstveniPregledKlasa pregled = new ZdravstveniPregledKlasa();

                pregled.ZdravstveniPregledID = Convert.ToInt32(red["ZdravstveniPregledID"]);
                pregled.ClanID = Convert.ToInt32(red["ClanID"]);
                pregled.DatumPregleda = Convert.ToDateTime(red["DatumPregleda"]);

                if (red["Napomena"] != DBNull.Value)
                    pregled.Napomena = red["Napomena"].ToString();
                else
                    pregled.Napomena = "";

                _lista.Add(pregled);
            }
        }
    }
}