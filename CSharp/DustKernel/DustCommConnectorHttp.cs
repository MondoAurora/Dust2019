/*
 * Created by SharpDevelop.
 * User: loran
 * Date: 19/05/30
 * Time: 11:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Dust.Kernel
{
	public static class DustCommConnectorHttp
	{
		private static readonly HttpClient HttpClient;

		static DustCommConnectorHttp()
		{
			HttpClient = new HttpClient();
			
			HttpClient.DefaultRequestHeaders.Accept.Clear();
			HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
	
		public static async void loadRemote(String serverAddr, String module, String entityId, DustEntityProcessorBase proc)
		{
			try {
				string responseBody = await HttpClient.GetStringAsync("http://" + serverAddr + "/GetEntity?RemoteRefModuleName=" + module + "&RemoteRefItemModuleId=" + entityId);
				DustEntityInstance entity = DustCommSerializerJson.loadSingleFromText(responseBody, module, entityId);
				proc.processEntity(entity);
			} catch (HttpRequestException e) {
				Console.WriteLine("\nException Caught!");	
				Console.WriteLine("Message :{0} ", e.Message);
			}
		}
	}
}
