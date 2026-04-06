using System;

namespace PoslovnaLogika
{
	public class StatusZdravstvenogPregledaKlasa
	{
		// konstruktor
		public StatusZdravstvenogPregledaKlasa()
		{

		}

		// public metode
		public string OdrediStatus(DateTime? datumPregledaParametar)
		{
			if (!datumPregledaParametar.HasValue)
			{
				return "Nema pregleda";
			}

			DateTime datumIsteka = datumPregledaParametar.Value.AddMonths(6);

			if (datumIsteka >= DateTime.Today)
			{
				return "Aktivan";
			}

			return "Neaktivan";
		}
	}
}