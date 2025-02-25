using System;

namespace I2.Loc
{
	public static class ScriptLocalization
	{
		public static string Get(string Term)
		{
			return LocalizationManager.GetTermTranslation(Term, LocalizationManager.IsRight2Left, 0, false);
		}

		public static string Get(string Term, bool FixForRTL)
		{
			return LocalizationManager.GetTermTranslation(Term, FixForRTL, 0, false);
		}

		public static string Get(string Term, bool FixForRTL, int maxLineLengthForRTL)
		{
			return LocalizationManager.GetTermTranslation(Term, FixForRTL, maxLineLengthForRTL, false);
		}

		public static string Get(string Term, bool FixForRTL, int maxLineLengthForRTL, bool ignoreNumbers)
		{
			return LocalizationManager.GetTermTranslation(Term, FixForRTL, maxLineLengthForRTL, ignoreNumbers);
		}
	}
}
