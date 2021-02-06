using System;
using TeleScope.Persistence.Abstractions;

namespace D3vS1m.Application.Antenna
{
	public class AntennaGainToCsvParser : IParsable<string[]>
	{
		public string[] Parse<Tin>(Tin input, int index = 0, int length = 1)
		{
			throw new NotImplementedException();
		}
	}
}
