using System;
using System.Data.SqlClient;
using System.Configuration;

namespace DBUtils
{
    public class KonekcijaKlasa
    {
        // Ime konekcije u app.config / web.config
        private const string PrimaryConnName = "VP2026KarateKlubZdravstveniPregledV1";
        private const string FallbackConnName = "NasaKonekcija";

        private SqlConnection _konekcija;

        private string _putanjaBaze;
        private string _nazivBaze;
        private string _nazivDBMSinstance;
        private string _stringKonekcije;

        public KonekcijaKlasa(string nazivDBMSInstance, string putanjaBaze, string nazivBaze)
        {
            _putanjaBaze = putanjaBaze;
            _nazivBaze = nazivBaze;
            _nazivDBMSinstance = nazivDBMSInstance;
            _stringKonekcije = "";
        }

        public KonekcijaKlasa(string noviStringKonekcije)
        {
            _putanjaBaze = "";
            _nazivBaze = "";
            _nazivDBMSinstance = "";
            _stringKonekcije = noviStringKonekcije;
        }

        private string DajStringKonekcije()
        {
            if (!string.IsNullOrWhiteSpace(_stringKonekcije))
                return _stringKonekcije;

            var cfgPrimary = ConfigurationManager.ConnectionStrings[PrimaryConnName]?.ConnectionString;
            if (!string.IsNullOrWhiteSpace(cfgPrimary))
                return cfgPrimary;

            var cfgFallback = ConfigurationManager.ConnectionStrings[FallbackConnName]?.ConnectionString;
            if (!string.IsNullOrWhiteSpace(cfgFallback))
                return cfgFallback;

            string pomStringKonekcije;

            if (string.IsNullOrEmpty(_putanjaBaze))
            {
                pomStringKonekcije = "Data Source=" + _nazivDBMSinstance +
                                     ";Initial Catalog=" + _nazivBaze +
                                     ";Integrated Security=True";
            }
            else
            {
                pomStringKonekcije = "Data Source=\\" + _nazivDBMSinstance +
                                     ";AttachDbFilename=" + _putanjaBaze + "\\" + _nazivBaze +
                                     ";Integrated Security=True;Connect Timeout=30;User Instance=True";
            }

            return pomStringKonekcije;
        }

        public bool OtvoriKonekciju()
        {
            bool uspeh;

            _konekcija = new SqlConnection();
            _konekcija.ConnectionString = DajStringKonekcije();

            try
            {
                _konekcija.Open();
                uspeh = true;
            }
            catch
            {
                uspeh = false;
            }

            return uspeh;
        }

        public SqlConnection DajKonekciju()
        {
            return _konekcija;
        }

        public void ZatvoriKonekciju()
        {
            _putanjaBaze = "";

            if (_konekcija != null)
            {
                _konekcija.Close();
                _konekcija.Dispose();
                _konekcija = null;
            }
        }
    }
}
