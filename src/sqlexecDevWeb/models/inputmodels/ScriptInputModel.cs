using System.ComponentModel.DataAnnotations;

namespace Sorling.sqlexecDevWeb.models.inputmodels;

public class ScriptInputModel
{
   [Required(AllowEmptyStrings = false, ErrorMessage = "Required: Script text")]
   [Display(Name = "Script")]
   public string? SqlScriptText { get; set; }
}
