﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using System.IO;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;

namespace MyView.Common
{
	/// <summary>
	/// Handles all requests to and from the Unsplash server.
	/// </summary>
	public class UnsplashAdapter
	{
		#region CONSTANTS
        /// The unique ID of this app 
		const string APP_ID = "5692dd4b4fe6468ed6adbccf3c531466bc8dd8f51676227b54213ea5bbe64d9e";
        const string SECRET = " 9a73c4df2713c2e7577e2b479e9aa9389465114c4b524a6b9de03304f44adbd1";

        const string BASE_API = "http://api.unsplash.com";
        const string CLIENT_AUTH = "client_id=" + APP_ID;
		#endregion


		#region PROPERTIES
		public static UnsplashAdapter Instance { get { return GetAdapterInstance(); } }
		#endregion


		#region VARIABLES
		private static UnsplashAdapter m_UnsplashAdapter;
        private static HttpClient m_HttpClient = new HttpClient();
        #endregion


        #region PUBLIC API
        /// <summary>
        /// Retrieves a random photo from the server.
        /// Note that a null photo is returned if the call fails.
        /// </summary>
        /// <returns></returns>
        public async Task<UnsplashImage> GetRandomPhoto()
        {
            var response = await m_HttpClient.GetAsync($"{BASE_API}/photos/random/?{CLIENT_AUTH}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                //dynamic photoObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                var photoObject = Newtonsoft.Json.JsonConvert.DeserializeObject<UnsplashImage>(jsonResponse);

                Debug.WriteLine("---" + (string)photoObject.urls.full);
            }
            else
            {
                Debug.WriteLine("GetRandomPhoto failed: " + response.Content.ReadAsStringAsync().Result);
            }

            return null;
        }
        #endregion


        #region HELPERS
        static UnsplashAdapter GetAdapterInstance()
		{
			if (m_UnsplashAdapter == null)
			{
				m_UnsplashAdapter = new UnsplashAdapter();
			}

			return m_UnsplashAdapter; 
		}
		#endregion
	}
}
