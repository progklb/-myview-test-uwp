using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;

namespace MyView.Common
{
	/// <summary>
	/// Handles all requests to and from the Unsplash server.
	/// </summary>
	public class UnsplashAdapter
	{
		#region CONSTANTS
        /// The unique ID of this app 
		const string CLIENT_ID = "5692dd4b4fe6468ed6adbccf3c531466bc8dd8f51676227b54213ea5bbe64d9e";

		const string BASE_API = "http://api.unsplash.com";
		const string ENDPOINT_RANDOM = "/photos/random";
		#endregion


		#region PROPERTIES
		public static UnsplashAdapter Instance { get { return GetAdapterInstance(); } }
		#endregion


		#region VARIABLES
		private static UnsplashAdapter m_UnsplashAdapter;
		#endregion


		#region PUBLIC API
        public async Task GetPhotoList()
        {
            var httpClient = new HttpClient();
            var jsonResponse = await httpClient.GetStringAsync("https://unsplash.it/list");
            Debug.WriteLine(jsonResponse);

            /*dynamic list = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);

            try
            { 
                foreach (var item in list)
                {
                    Debug.WriteLine(item.filename);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("--- Deserialisation object read failure");
            }*/
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
