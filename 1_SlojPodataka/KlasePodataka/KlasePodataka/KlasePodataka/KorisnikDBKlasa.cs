using System;
using System.Data;
using DBUtils;

namespace KlasePodataka
{
    public class KorisnikDBKlasa : TabelaKlasa
    {
        private readonly KonekcijaKlasa _konekcija;

        public KorisnikDBKlasa(string noviStringKonekcije)
            : this(KreirajOtvorenuKonekciju(noviStringKonekcije))
        {
        }

        private KorisnikDBKlasa(KonekcijaKlasa konekcija)
            : base(konekcija, "Korisnik")
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

        public KorisnikKlasa Prijava(string korisnickoImeParametar, string lozinkaParametar)
        {
            try
            {
                string upit = string.Format(
                    "SELECT TOP 1 KorisnikID, KorisnickoIme, Lozinka, Ime, Prezime FROM dbo.Korisnik WHERE KorisnickoIme = '{0}' AND Lozinka = '{1}'",
                    EscapirajSql(korisnickoImeParametar),
                    EscapirajSql(lozinkaParametar));

                DataSet ds = DajPodatke(upit);

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                DataRow red = ds.Tables[0].Rows[0];
                return new KorisnikKlasa
                {
                    KorisnikID = Convert.ToInt32(red["KorisnikID"]),
                    KorisnickoIme = red["KorisnickoIme"].ToString(),
                    Lozinka = red["Lozinka"].ToString(),
                    Ime = red["Ime"].ToString(),
                    Prezime = red["Prezime"].ToString()
                };
            }
            finally
            {
                _konekcija.ZatvoriKonekciju();
            }
        }
    }
}
