namespace Sorling.sqlexecCodeGen.helpers;

public static class IOHelper
{
   public static string LoadResourceString(string name) {
      Type type = typeof(IOHelper);
      using Stream? resstream = type.Assembly
      .GetManifestResourceStream(name ?? throw new ArgumentNullException(nameof(name)))
         ?? throw new FileNotFoundException($"Resource '{name}' not found in '{type.Assembly.FullName}'");

      using MemoryStream ms = new();
      resstream.CopyTo(ms);
      ms.Position = 0;
      using StreamReader sr = new(ms);
      return sr.ReadToEnd();
   }

   public static void SaveStringToFile(string path, string value) => File.WriteAllText(path, value);
}
