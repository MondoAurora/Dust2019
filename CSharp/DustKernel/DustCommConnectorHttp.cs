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
using System.Threading.Tasks;
using System.IO;

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
	
		public static async Task<DustEntity> loadRemote(String serverAddr, String module, String entityId)
		{
			try {
//							var tcs = new TaskCompletionSource<DustEntity>();

				string responseBody = await HttpClient.GetStringAsync("http://" + serverAddr + "/GetEntity?RemoteRefModuleName=" + module + "&RemoteRefItemModuleId=" + entityId);
				
				File.WriteAllText(module + "." + entityId + ".json", responseBody);
				
				DustDataEntity entity = DustCommSerializerJson.loadSingleFromText(responseBody, module, entityId);
//				proc.processEntity(entity);
//				tcs.TrySetResult(entity);
				return entity;
				
			} catch (HttpRequestException e) {
				Console.WriteLine("\nException Caught!");	
				Console.WriteLine("Message :{0} ", e.Message);
				return null;
			}
		}
	}
}
