using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PoslovnaLogika
{
    public class PregledZdravstvenogStatusaClanovaKlasa
    {
        // atribut
        private string _stringKonekcije;

        // konstruktor
        public PregledZdravstvenogStatusaClanovaKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        // public metoda
        public List<ClanStatusPregledaDTO> DajPregledClanovaSaStatusom()
        {
            List<ClanStatusPregledaDTO> rezultat = new List<ClanStatusPregledaDTO>();

            using (SqlConnection konekcija = new SqlConnection(_stringKonekcije))
            {
                using (SqlCommand komanda = new SqlCommand("sp_PrikazClanovaSaStatusomPregleda", konekcija))
                {
                    komanda.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(komanda))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        if (ds.Tables.Count == 0)
                        {
                            return rezultat;
                        }

                        StatusZdravstvenogPregledaKlasa statusKlasa = new StatusZdravstvenogPregledaKlasa();

                        foreach (DataRow red in ds.Tables[0].Rows)
                        {
                            ClanStatusPregledaDTO dto = new ClanStatusPregledaDTO();

                            dto.ClanID = Convert.ToInt32(red["ClanID"]);
                            dto.JMBG = red["JMBG"].ToString();
                            dto.Ime = red["Ime"].ToString();
                            dto.Prezime = red["Prezime"].ToString();
                            dto.DatumRodjenja = Convert.ToDateTime(red["DatumRodjenja"]);
                            dto.DatumPregleda = red["PoslednjiPregled"] != DBNull.Value
                                ? Convert.ToDateTime(red["PoslednjiPregled"])
                                : (DateTime?)null;
                            dto.StatusPregleda = statusKlasa.OdrediStatus(dto.DatumPregleda);

                            rezultat.Add(dto);
                        }
                    }
                }
            }

            return rezultat;
        }
    }
}
