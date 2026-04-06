using System;
using System.Data;
using System.Data.SqlClient;

namespace KlasePodataka
{
    public class SPZdravstveniPregledDBKlasa
    {
        private string _stringKonekcije;

        public string StringKonekcije
        {
            get { return _stringKonekcije; }
            set { _stringKonekcije = value; }
        }

        public SPZdravstveniPregledDBKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        public bool DodajZdravstveniPregled(ZdravstveniPregledKlasa pregledParametar)
        {
            bool uspeh = false;

            try
            {
                using (SqlConnection konekcija = new SqlConnection(_stringKonekcije))
                {
                    using (SqlCommand komanda = new SqlCommand("sp_DodajZdravstveniPregled", konekcija))
                    {
                        komanda.CommandType = CommandType.StoredProcedure;

                        komanda.Parameters.AddWithValue("@ClanID", pregledParametar.ClanID);
                        komanda.Parameters.AddWithValue("@DatumPregleda", pregledParametar.DatumPregleda);

                        if (string.IsNullOrWhiteSpace(pregledParametar.Napomena))
                            komanda.Parameters.AddWithValue("@Napomena", DBNull.Value);
                        else
                            komanda.Parameters.AddWithValue("@Napomena", pregledParametar.Napomena);

                        konekcija.Open();
                        komanda.ExecuteNonQuery();
                        uspeh = true;
                    }
                }
            }
            catch
            {
                uspeh = false;
            }

            return uspeh;
        }

        public bool IzmeniZdravstveniPregled(ZdravstveniPregledKlasa pregledParametar)
        {
            bool uspeh = false;

            try
            {
                using (SqlConnection konekcija = new SqlConnection(_stringKonekcije))
                {
                    using (SqlCommand komanda = new SqlCommand("sp_IzmeniZdravstveniPregled", konekcija))
                    {
                        komanda.CommandType = CommandType.StoredProcedure;

                        komanda.Parameters.AddWithValue("@ZdravstveniPregledID", pregledParametar.ZdravstveniPregledID);
                        komanda.Parameters.AddWithValue("@ClanID", pregledParametar.ClanID);
                        komanda.Parameters.AddWithValue("@DatumPregleda", pregledParametar.DatumPregleda);

                        if (string.IsNullOrWhiteSpace(pregledParametar.Napomena))
                            komanda.Parameters.AddWithValue("@Napomena", DBNull.Value);
                        else
                            komanda.Parameters.AddWithValue("@Napomena", pregledParametar.Napomena);

                        konekcija.Open();
                        komanda.ExecuteNonQuery();
                        uspeh = true;
                    }
                }
            }
            catch
            {
                uspeh = false;
            }

            return uspeh;
        }

        public bool ObrisiZdravstveniPregled(int zdravstveniPregledIDParametar)
        {
            bool uspeh = false;

            try
            {
                using (SqlConnection konekcija = new SqlConnection(_stringKonekcije))
                {
                    using (SqlCommand komanda = new SqlCommand("sp_ObrisiZdravstveniPregled", konekcija))
                    {
                        komanda.CommandType = CommandType.StoredProcedure;
                        komanda.Parameters.AddWithValue("@ZdravstveniPregledID", zdravstveniPregledIDParametar);

                        konekcija.Open();
                        komanda.ExecuteNonQuery();
                        uspeh = true;
                    }
                }
            }
            catch
            {
                uspeh = false;
            }

            return uspeh;
        }

        public ZdravstveniPregledListaKlasa DajSveZdravstvenePreglede()
        {
            ZdravstveniPregledListaKlasa listaObjekat = new ZdravstveniPregledListaKlasa();

            try
            {
                using (SqlConnection konekcija = new SqlConnection(_stringKonekcije))
                {
                    using (SqlCommand komanda = new SqlCommand("sp_DajSveZdravstvenePreglede", konekcija))
                    {
                        komanda.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(komanda))
                        {
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            listaObjekat.NapuniListu(ds);
                        }
                    }
                }
            }
            catch
            {
            }

            return listaObjekat;
        }

        public ZdravstveniPregledListaKlasa DajPregledeZaClana(int clanIDParametar)
        {
            ZdravstveniPregledListaKlasa listaObjekat = new ZdravstveniPregledListaKlasa();

            try
            {
                using (SqlConnection konekcija = new SqlConnection(_stringKonekcije))
                {
                    using (SqlCommand komanda = new SqlCommand("sp_DajPregledeZaClana", konekcija))
                    {
                        komanda.CommandType = CommandType.StoredProcedure;
                        komanda.Parameters.AddWithValue("@ClanID", clanIDParametar);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(komanda))
                        {
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            listaObjekat.NapuniListu(ds);
                        }
                    }
                }
            }
            catch
            {
            }

            return listaObjekat;
        }

        public ZdravstveniPregledKlasa DajPoslednjiPregledZaClana(int clanIDParametar)
        {
            ZdravstveniPregledKlasa pregledObjekat = null;

            try
            {
                using (SqlConnection konekcija = new SqlConnection(_stringKonekcije))
                {
                    using (SqlCommand komanda = new SqlCommand("sp_DajPoslednjiPregledZaClana", konekcija))
                    {
                        komanda.CommandType = CommandType.StoredProcedure;
                        komanda.Parameters.AddWithValue("@ClanID", clanIDParametar);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(komanda))
                        {
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                DataRow red = ds.Tables[0].Rows[0];

                                pregledObjekat = new ZdravstveniPregledKlasa();
                                pregledObjekat.ZdravstveniPregledID = Convert.ToInt32(red["ZdravstveniPregledID"]);
                                pregledObjekat.ClanID = Convert.ToInt32(red["ClanID"]);
                                pregledObjekat.DatumPregleda = Convert.ToDateTime(red["DatumPregleda"]);
                                pregledObjekat.Napomena = red["Napomena"] != DBNull.Value ? red["Napomena"].ToString() : "";
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return pregledObjekat;
        }
    }
}
