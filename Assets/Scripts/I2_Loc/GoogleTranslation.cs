using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	public static class GoogleTranslation
	{
		private sealed class _WaitForTranslation_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal WWW www;

			internal Action<string> OnTranslationReady;

			internal string OriginalText;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _WaitForTranslation_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = this.www;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					if (!string.IsNullOrEmpty(this.www.error))
					{
						UnityEngine.Debug.LogError(this.www.error);
						this.OnTranslationReady(string.Empty);
					}
					else
					{
						byte[] bytes = this.www.bytes;
						string @string = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
						string obj = GoogleTranslation.ParseTranslationResult(@string, this.OriginalText);
						this.OnTranslationReady(obj);
					}
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _WaitForTranslation_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal WWW www;

			internal Action<List<TranslationRequest>> OnTranslationReady;

			internal List<TranslationRequest> requests;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _WaitForTranslation_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = this.www;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					if (!string.IsNullOrEmpty(this.www.error))
					{
						UnityEngine.Debug.LogError(this.www.error);
						this.OnTranslationReady(this.requests);
					}
					else
					{
						byte[] bytes = this.www.bytes;
						string @string = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
						GoogleTranslation.ParseTranslationResult(@string, this.requests);
						this.OnTranslationReady(this.requests);
					}
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		public static bool CanTranslate()
		{
			return LocalizationManager.Sources.Count > 0 && !string.IsNullOrEmpty(LocalizationManager.GetWebServiceURL(null));
		}

		public static void Translate(string text, string LanguageCodeFrom, string LanguageCodeTo, Action<string> OnTranslationReady)
		{
			WWW translationWWW = GoogleTranslation.GetTranslationWWW(text, LanguageCodeFrom, LanguageCodeTo);
			CoroutineManager.pInstance.StartCoroutine(GoogleTranslation.WaitForTranslation(translationWWW, OnTranslationReady, text));
		}

		private static IEnumerator WaitForTranslation(WWW www, Action<string> OnTranslationReady, string OriginalText)
		{
			GoogleTranslation._WaitForTranslation_c__Iterator0 _WaitForTranslation_c__Iterator = new GoogleTranslation._WaitForTranslation_c__Iterator0();
			_WaitForTranslation_c__Iterator.www = www;
			_WaitForTranslation_c__Iterator.OnTranslationReady = OnTranslationReady;
			_WaitForTranslation_c__Iterator.OriginalText = OriginalText;
			return _WaitForTranslation_c__Iterator;
		}

		public static string ForceTranslate(string text, string LanguageCodeFrom, string LanguageCodeTo)
		{
			WWW translationWWW = GoogleTranslation.GetTranslationWWW(text, LanguageCodeFrom, LanguageCodeTo);
			while (!translationWWW.isDone)
			{
			}
			if (!string.IsNullOrEmpty(translationWWW.error))
			{
				UnityEngine.Debug.LogError("-- " + translationWWW.error);
				return string.Empty;
			}
			byte[] bytes = translationWWW.bytes;
			string @string = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
			return GoogleTranslation.ParseTranslationResult(@string, text);
		}

		public static WWW GetTranslationWWW(string text, string LanguageCodeFrom, string LanguageCodeTo)
		{
			LanguageCodeFrom = GoogleLanguages.GetGoogleLanguageCode(LanguageCodeFrom);
			LanguageCodeTo = GoogleLanguages.GetGoogleLanguageCode(LanguageCodeTo);
			if (GoogleTranslation.TitleCase(text) == text && text.ToUpper() != text)
			{
				text = text.ToLower();
			}
			string url = string.Format("{0}?action=Translate&list={1}:{2}={3}", new object[]
			{
				LocalizationManager.GetWebServiceURL(null),
				LanguageCodeFrom,
				LanguageCodeTo,
				Uri.EscapeUriString(text)
			});
			return new WWW(url);
		}

		public static string ParseTranslationResult(string html, string OriginalText)
		{
			string result;
			try
			{
				string text = html;
				if (GoogleTranslation.TitleCase(OriginalText) == OriginalText && OriginalText.ToUpper() != OriginalText)
				{
					text = GoogleTranslation.TitleCase(text);
				}
				result = text;
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError(ex.Message);
				result = string.Empty;
			}
			return result;
		}

		public static void Translate(List<TranslationRequest> requests, Action<List<TranslationRequest>> OnTranslationReady)
		{
			WWW translationWWW = GoogleTranslation.GetTranslationWWW(requests);
			CoroutineManager.pInstance.StartCoroutine(GoogleTranslation.WaitForTranslation(translationWWW, OnTranslationReady, requests));
		}

		public static WWW GetTranslationWWW(List<TranslationRequest> requests)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (TranslationRequest current in requests)
			{
				if (!flag)
				{
					stringBuilder.Append("<I2Loc>");
				}
				stringBuilder.Append(current.LanguageCode);
				stringBuilder.Append(":");
				for (int i = 0; i < current.TargetLanguagesCode.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(current.TargetLanguagesCode[i]);
				}
				stringBuilder.Append("=");
				string stringToEscape = (!(GoogleTranslation.TitleCase(current.Text) == current.Text)) ? current.Text : current.Text.ToLowerInvariant();
				stringBuilder.Append(Uri.EscapeUriString(stringToEscape));
				flag = false;
				if (stringBuilder.Length > 4000)
				{
					break;
				}
			}
			return new WWW(string.Format("{0}?action=Translate&list={1}", LocalizationManager.GetWebServiceURL(null), stringBuilder.ToString()));
		}

		private static IEnumerator WaitForTranslation(WWW www, Action<List<TranslationRequest>> OnTranslationReady, List<TranslationRequest> requests)
		{
			GoogleTranslation._WaitForTranslation_c__Iterator1 _WaitForTranslation_c__Iterator = new GoogleTranslation._WaitForTranslation_c__Iterator1();
			_WaitForTranslation_c__Iterator.www = www;
			_WaitForTranslation_c__Iterator.OnTranslationReady = OnTranslationReady;
			_WaitForTranslation_c__Iterator.requests = requests;
			return _WaitForTranslation_c__Iterator;
		}

		public static string ParseTranslationResult(string html, List<TranslationRequest> requests)
		{
			if (!html.StartsWith("<!DOCTYPE html>") && !html.StartsWith("<HTML>"))
			{
				string[] array = html.Split(new string[]
				{
					"<I2Loc>"
				}, StringSplitOptions.None);
				string[] separator = new string[]
				{
					"<i2>"
				};
				for (int i = 0; i < Mathf.Min(requests.Count, array.Length); i++)
				{
					TranslationRequest value = requests[i];
					value.Results = array[i].Split(separator, StringSplitOptions.None);
					if (GoogleTranslation.TitleCase(value.Text) == value.Text)
					{
						for (int j = 0; j < value.Results.Length; j++)
						{
							value.Results[j] = GoogleTranslation.TitleCase(value.Results[j]);
						}
					}
					requests[i] = value;
				}
				return string.Empty;
			}
			if (html.Contains("Service invoked too many times in a short time"))
			{
				return string.Empty;
			}
			return "There was a problem contacting the WebService. Please try again later";
		}

		public static string UppercaseFirst(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			char[] array = s.ToLower().ToCharArray();
			array[0] = char.ToUpper(array[0]);
			return new string(array);
		}

		public static string TitleCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
		}
	}
}
