using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.ComponentModel;
using System.Drawing;
//using Dapper;

namespace nclprospekt.DAL
{
    public static class Helpers
    {
      public enum ListaTypeEnum
        {
            ID,
            WARTOSC_INT,
            WARTOSC_NVARCHAR,
            WARTOSC_BIN
        }

        public static DataTable getListaTypeTbl()
        {
            DataTable listaTypeTable = new DataTable();
            listaTypeTable.TableName = "tblListaTypeTbl";
            listaTypeTable.Columns.Add(Enum.GetName(typeof(ListaTypeEnum), ListaTypeEnum.ID), Type.GetType("System.Int32"));
            listaTypeTable.Columns.Add(Enum.GetName(typeof(ListaTypeEnum), ListaTypeEnum.WARTOSC_INT), Type.GetType("System.Int32"));
            listaTypeTable.Columns.Add(Enum.GetName(typeof(ListaTypeEnum), ListaTypeEnum.WARTOSC_NVARCHAR), Type.GetType("System.String"));
            listaTypeTable.Columns.Add(Enum.GetName(typeof(ListaTypeEnum), ListaTypeEnum.WARTOSC_BIN), Type.GetType("System.Byte[]"));

            listaTypeTable.Columns[Enum.GetName(typeof(ListaTypeEnum), ListaTypeEnum.ID)].SetOrdinal(0);
            listaTypeTable.Columns[Enum.GetName(typeof(ListaTypeEnum), ListaTypeEnum.WARTOSC_INT)].SetOrdinal(1);
            listaTypeTable.Columns[Enum.GetName(typeof(ListaTypeEnum), ListaTypeEnum.WARTOSC_NVARCHAR)].SetOrdinal(2);
            listaTypeTable.Columns[Enum.GetName(typeof(ListaTypeEnum), ListaTypeEnum.WARTOSC_BIN)].SetOrdinal(3);
            return listaTypeTable;
        }
   
        public static Type getClassInstance(String fileName, string className)
        {
            /* Load in the assembly. */
            System.Reflection.Assembly moduleAssembly = System.Reflection.Assembly.LoadFile(fileName);

            /* Get the types of classes that are in this assembly. */
            Type[] types = moduleAssembly.GetTypes();

            /* Loop through the types in the assembly until we find
             * a class that implements a Module.
             */
            foreach (Type type in types)
            {
                if (type.Name == className)
                {
                    return type;
                }
            }

            return null;
        }

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            if (list != null) //Lista czasem może być pusta - dodatkowe zabezpieczenie
            { 
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }
            }
            return ds;
        }
public static bool IsValidImage(byte[] bytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                    Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }


        public static string FixBase64Length(string additionalQueryStringEncoded)
        {
            int length = additionalQueryStringEncoded.Length;
            int remainder = length % 4;
            if (remainder == 0)
            {
                return additionalQueryStringEncoded.Replace(" ", "+");
            }

            remainder = 4 - remainder;
            for (int i = 0; i < remainder; i++)
            {
                additionalQueryStringEncoded += "=";
            }

            return additionalQueryStringEncoded.Replace(" ", "+");

        }

        private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        #region Big freaking list of mime types
        {".doc", "application/msword"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".pdf", "application/pdf"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},

        #endregion

        };

        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

    }
}