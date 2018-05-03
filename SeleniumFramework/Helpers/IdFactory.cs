using System;
using System.Diagnostics;
using System.Text;
using SeleniumFramework.Helpers;

namespace SeleniumFramework.Helpers
{
    public class IdFactory
    {

        public static string Generate(IdSegments[] idSegments, string uniqueId, string delimiter = "_")
        {
            var sb = new StringBuilder();

            Array.ForEach(idSegments, s => sb.Append($"{enumToString(s)}{delimiter}"));
            
            sb.Append(uniqueId);
           return sb.ToString();
        }

        public static string Generate(IdSegments[] idSegments, UniqueIds uniqueId, string delimiter = "_")
        {
            return Generate(idSegments, enumToString(uniqueId), delimiter);
        }

        private static string enumToString(IdSegments enumToConvert)
        {
            return Enum.GetName(typeof(IdSegments), enumToConvert);
        }

        private static string enumToString(UniqueIds enumToConvert)
        {
            return Enum.GetName(typeof(UniqueIds), enumToConvert);
        }

    }

}