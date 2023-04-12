using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.UI.DevExpress.Shared.Enums
{
    public enum TipoFormato
    {
        formNenhum = 0, formData, formCPF, formCGC, formTelefone, formCEP, formMonetario, formHora
    }
    public enum TipoRestricao
    {
        restrNenhuma = FormatType.None,
        restrLetras,
        restrNumeros,
        restrValores
    }
    public enum EditorType
    {
        TextEdit = 0,
        ComboEdit,
        ComboEditLookUp,
        ComboEditImage,
        CheckEdit
    }
    public enum ButtonType
    {
        User = 0,
        Save,
        Edit,
        Cancel,
        Find,
        Exit
    }
}
