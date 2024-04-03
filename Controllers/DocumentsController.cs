using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EditorOfficeBlazorServer3.Models; // Asegúrate de usar el espacio de nombres correcto para UserData

namespace EditorOfficeBlazorServer3.Controllers // Cambia esto por tu espacio de nombres correcto
{
    [ApiController]
    [Route("api/[action]")]
    public class DocumentsController : ControllerBase
    {
        private readonly string _docsPath = @"C:\inetpub\wwwroot";
        private readonly string _modsPath = @"C:\inetpub\wwwroot\tmp";

        public DocumentsController()
        {
            // Asegúrate de que los directorios existan
            if (!Directory.Exists(_modsPath))
            {
                Directory.CreateDirectory(_modsPath);
            }
        }

        // [HttpPost("ProcessTemplate")]
        [HttpPost]
        public ActionResult<string> ProcessTemplate([FromBody] UserData userData)
        {
            // Encuentra la ruta del archivo de la plantilla
            var templatePath = Path.Combine(_docsPath, userData.TemplateName);
            if (!System.IO.File.Exists(templatePath))
            {
                return NotFound("Plantilla no encontrada.");
            }

            // Genera un nombre único para el archivo modificado
            var modifiedFileName = $"modificado_{DateTime.Now.Ticks}.docx";
            var modifiedFilePath = Path.Combine(_modsPath, modifiedFileName);

            // Primero, copia el archivo de plantilla al directorio de modificaciones
            System.IO.File.Copy(templatePath, modifiedFilePath, true);

            // Luego, abre el archivo copiado para edición
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(modifiedFilePath, true))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;
                ReplaceTextInBody(body, "{Nombre}", userData.Nombre);
                ReplaceTextInBody(body, "{Cedula}", userData.Cedula);
                ReplaceTextInBody(body, "{Direccion}", userData.Direccion);

                wordDoc.MainDocumentPart.Document.Save(); // Guarda los cambios en el archivo copiado
            }

            // Retorna el nombre del archivo modificado
            return Ok(modifiedFileName);
        }

        private void ReplaceTextInBody(Body body, string toReplace, string text)
        {
            foreach (var textElement in body.Descendants<Text>())
            {
                if (textElement.Text.Contains(toReplace))
                {
                    textElement.Text = textElement.Text.Replace(toReplace, text);
                }
            }
        }

        // Añade aquí más métodos según sea necesario, como por ejemplo un método para servir los documentos modificados
    }
}
