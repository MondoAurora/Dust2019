/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/12
 * Time: 08:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Dust;

namespace Dust.Module
{
	public class DustModule : Dust
	{
		public static void initKernel(DustKernel kernel)
		{
			Dust.init(kernel);
		}
	}
}
