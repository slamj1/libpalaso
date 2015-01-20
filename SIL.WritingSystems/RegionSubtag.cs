﻿namespace SIL.WritingSystems
{
	public class RegionSubtag : Subtag
	{
		public RegionSubtag(string code, bool isPrivateUse)
			: base(code, isPrivateUse)
		{
		}

		public RegionSubtag(string code, string name, bool isPrivateUse)
			: base(code, name, isPrivateUse)
		{
		}

		public static implicit operator RegionSubtag(string code)
		{
			if (string.IsNullOrEmpty(code))
				return null;

			RegionSubtag subtag;
			if (!StandardSubtags.Iso3166Regions.TryGetItem(code, out subtag))
				subtag = new RegionSubtag(code, true);
			return subtag;
		}
	}
}