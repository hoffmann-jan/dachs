using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using dachs.Interfaces;
using HtmlAgilityPack;

namespace dachs
{
    /// <summary>
    /// Extractor.
    /// </summary>
    public class Extractor : IExtractor
    {
        #region Fields
        /// <summary>
        /// The URL.
        /// </summary>
        private string _Url = @"https://adressen.leipzig.de";

        /// <summary>
        /// Parameter für die Abfrage.
        /// </summary>
        private string __EVENTVALIDATION;
        /// <summary>
        /// Parameter für die Abfrage.
        /// </summary>
        private string __VIEWSTATE;
        /// <summary>
        /// Parameter für die Abfrage.
        /// </summary>
        private string __VIEWSTATEGENERATOR;
        /// <summary>
        /// Parameter für die Abfrage.
        /// </summary>
        private string btnFindStreet;
        /// <summary>
        /// Hausnummer.
        /// </summary>
        private string txtHnr;
        /// <summary>
        /// Straßenname.
        /// </summary>
        string txtStreet;

        /// <summary>
        /// The has error.
        /// </summary>
        private bool _HasError = false;
        /// <summary>
        /// The exception.
        /// </summary>
        private Exception _Exception = null;

        /// <summary>
        /// Http Client.
        /// </summary>
        private static readonly HttpClient client = new HttpClient();
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:dachs.Extractor"/> has error.
        /// </summary>
        /// <value><c>true</c> if has error; otherwise, <c>false</c>.</value>
        public bool HasError
        {
            get => _HasError;
            private set => _HasError = value;
        }
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception 
        { 
            get => _Exception;
            private set
            {
                if (value != null)
                    HasError = true;
                _Exception = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:dachs.Extractor"/> class.
        /// </summary>
        public Extractor() 
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:dachs.Extractor"/> class.
        /// </summary>
        /// <param name="url">URL.</param>
        public Extractor(string url)
        {
            _Url = url;

            Init();
        }
        #endregion

        #region IExtractor
        IEnumerable<string> IExtractor.Extract(string streetOfLe)
        {
            txtStreet = streetOfLe;
            return GetAllNumbers();
        }
        #endregion

        /// <summary>
        /// Init this instance.
        /// </summary>
        private void Init()
        {
            if (!TrySetVariables())
                HasError = true;
        }

        /// <summary>
        /// Tries the set variables.
        /// </summary>
        /// <returns><c>true</c>, if set variables was tryed, <c>false</c> otherwise.</returns>
        private bool TrySetVariables()
        {
            try
            {
                Stream httpResult = client.GetStreamAsync(_Url).Result;

                HtmlNode node;                
                HtmlDocument doc = new HtmlDocument();
                doc.Load(httpResult);
                node = doc.GetElementbyId("__VIEWSTATE");
                __VIEWSTATE = node.GetAttributeValue("value", string.Empty);
                node = doc.GetElementbyId("__VIEWSTATEGENERATOR");
                __VIEWSTATEGENERATOR = node.GetAttributeValue("value", string.Empty);
                node = doc.GetElementbyId("__EVENTVALIDATION");
                __EVENTVALIDATION = node.GetAttributeValue("value", string.Empty);
                node = doc.GetElementbyId("btnFindStreet");
                btnFindStreet = node.GetAttributeValue("value", string.Empty);
                ReplaceSpaceChar(ref btnFindStreet);

                txtHnr = string.Empty;
                txtStreet = string.Empty;

            }
            catch (Exception ex)
            {
                Exception = ex;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Hollt alle Hausnummern der Straße.
        /// </summary>
        /// <returns>Liste aller Hausnummern.</returns>
        private IEnumerable<string> GetAllNumbers()
        {
            try
            {
                Stream response = Post(_Url, new Dictionary<string, string>() {
                    { nameof(__EVENTVALIDATION), __EVENTVALIDATION },
                    { nameof(__VIEWSTATE), __VIEWSTATE },
                    { nameof(__VIEWSTATEGENERATOR), __VIEWSTATEGENERATOR },
                    { nameof(btnFindStreet), btnFindStreet },
                    { nameof(txtHnr), string.Empty },
                    { nameof(txtStreet), txtStreet }
                });

                List<string> result = new List<string>();

                HtmlDocument doc = new HtmlDocument();
                doc.Load(response);

                if (TryGetMatchResult("(option value)([=\\\" >])+([\\d]+)", doc.Text, out MatchCollection matches))
                {
                    foreach (Match match in matches)
                    {
                        result.Add(match.Groups[3].Value);
                    }
                }


                return result;
            }
            catch (Exception ex)
            {
                Exception = ex;
            }

            return null;
        }

        /// <summary>
        /// Get all available streets of Le.
        /// </summary>
        /// <returns>All available streets.</returns>
        public IEnumerable<string> ExtractAllStreets()
        {
            try
            {
                Stream response = Post(_Url, new Dictionary<string, string>() {
                    { nameof(__EVENTVALIDATION), __EVENTVALIDATION },
                    { nameof(__VIEWSTATE), __VIEWSTATE },
                    { nameof(__VIEWSTATEGENERATOR), __VIEWSTATEGENERATOR },
                    { nameof(btnFindStreet), "Neue+suche" },
                    { nameof(txtHnr), string.Empty },
                    { nameof(txtStreet), string.Empty }
                });

                List<string> result = new List<string>();

                HtmlDocument doc = new HtmlDocument();
                doc.Load(response);

                if (TryGetMatchResult("(option value)([=\" \\d]+)>([\\w #\\-&;.]+)<", doc.Text, out MatchCollection matches))
                {
                    string value;
                    foreach (Match match in matches)
                    {
                        value = match.Groups[3].Value;

                        while (value.Contains('&'))
                        {
                            int pos = value.IndexOf('&');
                            string charValue = value.Substring(pos + 2, 3);
                            value = value.Remove(pos, 6);

                            int charNum = int.Parse(charValue);

                            switch (charNum)
                            {
                                case 228:
                                    value = value.Insert(pos, "ä");
                                    break;
                                case 196:
                                    value = value.Insert(pos, "Ä");
                                    break;
                                case 214:
                                    value = value.Insert(pos, "Ö");
                                    break;
                                case 246:
                                    value = value.Insert(pos, "ö");
                                    break;
                                case 223:
                                    value = value.Insert(pos, "ß");
                                    break;
                                case 220:
                                    value = value.Insert(pos, "Ü");
                                    break;
                                case 252:
                                    value = value.Insert(pos, "ü");
                                    break;
                            }
                        }

                        result.Add(value);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }



        /// <summary>
        /// Abfrage vom Server.
        /// </summary>
        /// <param name="uri">URL.</param>
        /// <param name="values">Parameter.</param>
        /// <returns>Webseite als String.</returns>
        public Stream Post(string uri, Dictionary<string, string> values)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = client.PostAsync(uri, content).Result;

            return response.Content.ReadAsStreamAsync().Result;
        }


        /// <summary>
        /// Tries the get match result.
        /// </summary>
        /// <returns><c>true</c>, if get match result was tryed, <c>false</c> otherwise.</returns>
        /// <param name="pattern">Pattern.</param>
        /// <param name="input">Input.</param>
        /// <param name="matches">Match.</param>
        private bool TryGetMatchResult(string pattern, string input, out MatchCollection matches)
        {
            Regex regex = new Regex(pattern);
            matches = null;

            if (regex.IsMatch(input))
            {
                matches = regex.Matches(input);
                return true;
            }

            Exception = new Exception($"There is no match for:'{pattern}'");

            return false;
        }

        /// <summary>
        /// Replaces the space char.
        /// </summary>
        /// <param name="value">Value.</param>
        private void ReplaceSpaceChar(ref string value)
        {
            value = value.Replace(' ', '+');
        }

    }
}
