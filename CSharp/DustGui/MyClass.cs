/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/12
 * Time: 07:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Dust.Units.Generic;

namespace Dust.Gui
{
	public class MyClass
	{
		public void testFunction()
		{
			Console.WriteLine("Hello World from dll!");
			
			String f3 = DustUtils.getValue(DustContext.SELF, "what?", GenericAtts.IdentifiedIdLocal);
			
			Console.WriteLine("The main entity ID is {0}", f3);
		}
	}
}