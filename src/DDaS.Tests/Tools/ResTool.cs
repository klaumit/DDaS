using System;
using System.IO;

namespace DDaS.Tests.Tools
{
    public static class ResTool
    {
        public static byte[] Load(string name, Type? type = null)
        {
            var myType = type ?? typeof(ResTool);
            var myLoc = Path.GetFullPath(myType.Assembly.Location);
            var myDir = Path.GetDirectoryName(myLoc)!;
            var myRes = Path.Combine(myDir, "Resources");
            var myFile = Path.Combine(myRes, name);
            var myBytes = File.ReadAllBytes(myFile);
            return myBytes;
        }
    }
}