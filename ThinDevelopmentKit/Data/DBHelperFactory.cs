using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ThinDevelopmentKit.Config;

namespace ThinDevelopmentKit.Data
{
	/// <summary>
    /// 数据库操作对象工厂
	/// </summary>
	public static class DBHelperFactory
	{
        private static Dictionary<DatabaseType, Type> helperTypeDict;

        static DBHelperFactory()
        {
            helperTypeDict = new Dictionary<DatabaseType, Type>();
        }

		public static IDBHelper Create(DBConfig config)
		{
            string libFile = string.Format("ThinDevelopmentKit.Data.{0}.dll", config.DBType);
            Type helperType;
            if (helperTypeDict.ContainsKey(config.DBType))
            {
                helperType = helperTypeDict[config.DBType];
            }
            else
            {
                libFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, libFile);
                Assembly helperLib = Assembly.LoadFile(libFile);
                helperType = helperLib.GetType(string.Format("ThinDevelopmentKit.Data.{0}.{0}Helper", config.DBType));
                helperTypeDict.Add(config.DBType, helperType);
            }
            AbstractDBHelper dbHelper = Activator.CreateInstance(helperType, config) as AbstractDBHelper;
			return dbHelper;
		}
	}
}
