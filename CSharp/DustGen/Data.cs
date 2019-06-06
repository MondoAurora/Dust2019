/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.Data
{
	public class DataTypes : DustKey
	{
		private DataTypes(string module, int key) : base(module, key) { }

		public static DataTypes Message = new DataTypes("Data", 5);
		public static DataTypes Entity = new DataTypes("Data", 2);
	}

	public class DataAtts : DustKey
	{
		private DataAtts(string module, int key) : base(module, key) { }

		public static DataAtts MessageReturn = new DataAtts("Data", 10);
		public static DataAtts EntityBinaries = new DataAtts("Data", 6);
	}

	public class DataLinks : DustKey
	{
		private DataLinks(string module, int key) : base(module, key) { }

		public static DataLinks MessageTarget = new DataLinks("Data", 12);
		public static DataLinks EntityServices = new DataLinks("Data", 7);
		public static DataLinks EntityModels = new DataLinks("Data", 1);
		public static DataLinks EntityAccessControl = new DataLinks("Data", 9);
		public static DataLinks MessageSource = new DataLinks("Data", 11);
		public static DataLinks MessageCommand = new DataLinks("Data", 8);
		public static DataLinks EntityTags = new DataLinks("Data", 3);
		public static DataLinks EntityPrimaryType = new DataLinks("Data", 0);
	}

}
