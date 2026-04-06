using System;
using System.Data;
using DBUtils;

namespace KlasePodataka
{
    public class ClanDBKlasa : TabelaKlasa
    {
        private readonly KonekcijaKlasa _konekcija;

        public ClanDBKlasa(string noviStringKonekcije)
            : this(KreirajOtvorenuKonekciju(noviStringKonekcije))
        {
        }

        private ClanDBKlasa(KonekcijaKlasa konekcija)
            : base(konekcija, "Clan")
        {
            _konekcija = konekcija;
        }

        private static KonekcijaKlasa KreirajOtvorenuKonekciju(string stringKonekcije)
        {
            KonekcijaKlasa konekcija = new KonekcijaKlasa(stringKonekcije);

            if (!konekcija.OtvoriKonekciju())
            {
                throw new InvalidOperationException("Nije moguce otvoriti konekciju ka bazi podataka.");
            }

            return konekcija;
        }

        private static string EscapirajSql(string vrednost)
        {
            return (vrednost ?? string.Empty).Replace("'", "''");
        }

        public bool DodajClana(ClanKlasa clanParametar)
        {
            try
            {
                string upit = string.Format(
                    "INSERT INTO dbo.Clan (JMBG, Ime, Prezime, DatumRodjenja) VALUES ('{0}', '{1}', '{2}', '{3:yyyy-MM-dd}')",
                    EscapirajSql(clanParametar.JMBG),
                    EscapirajSql(clanParametar.Ime),
                    EscapirajSql(clanParametar.Prezime),
                    clanParametar.DatumRodjenja);

                return IzvrsiAzuriranje(upit);
            }
            finally
            {
                _konekcija.ZatvoriKonekciju();
            }
        }

        public bool IzmeniClana(ClanKlasa clanParametar)
        {
            try
            {
                string upit = string.Format(
                    "UPDATE dbo.Clan SET JMBG = '{0}', Ime = '{1}', Prezime = '{2}', DatumRodjenja = '{3:yyyy-MM-dd}' WHERE ClanID = {4}",
                    EscapirajSql(clanParametar.JMBG),
                    EscapirajSql(clanParametar.Ime),
                    EscapirajSql(clanParametar.Prezime),
                    clanParametar.DatumRodjenja,
                    clanParametar.ClanID);

                return IzvrsiAzuriranje(upit);
            }
            finally
            {
                _konekcija.ZatvoriKonekciju();
            }
        }

        public bool ObrisiClana(int clanIDParametar)
        {
            try
            {
                return IzvrsiAzuriranje("DELETE FROM dbo.Clan WHERE ClanID = " + clanIDParametar);
            }
            finally
            {
                _konekcija.ZatvoriKonekciju();
            }
        }

        public ClanListaKlasa DajSveClanove()
        {
            try
            {
                DataSet ds = DajPodatke(
                    "SELECT ClanID, JMBG, Ime, Prezime, DatumRodjenja FROM dbo.Clan ORDER BY Prezime, Ime");

                ClanListaKlasa lista = new ClanListaKlasa();
                lista.NapuniListu(ds);
                return lista;
            }
            finally
            {
                _konekcija.ZatvoriKonekciju();
            }
        }

        public ClanKlasa DajClanaPoID(int clanIDParametar)
        {
            try
            {
                DataSet ds = DajPodatke(
                    "SELECT ClanID, JMBG, Ime, Prezime, DatumRodjenja FROM dbo.Clan WHERE ClanID = " + clanIDParametar);

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                DataRow red = ds.Tables[0].Rows[0];
                return new ClanKlasa
                {
                    ClanID = Convert.ToInt32(red["ClanID"]),
                    JMBG = red["JMBG"].ToString(),
                    Ime = red["Ime"].ToString(),
                    Prezime = red["Prezime"].ToString(),
                    DatumRodjenja = Convert.ToDateTime(red["DatumRodjenja"])
                };
            }
            finally
            {
                _konekcija.ZatvoriKonekciju();
            }
        }
    }
}
