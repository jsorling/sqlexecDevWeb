namespace Sorling.sqlexecDevWeb.models;

public interface IPageMenu
{
   public IEnumerable<string>? DBSchemas { get; }

   public string? DBSchema { get; }
}
