using Microsoft.AspNetCore.Mvc;
using MongoTest.Models;
using MongoTest.Services;

namespace MongoTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _docsService;

        public DocumentController(DocumentService docsService) =>
            _docsService = docsService;

        [HttpGet]
        [ActionName(nameof(GetDocuments))]
        public async Task<List<Document>> GetDocuments() =>
            await _docsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        [ActionName(nameof(GetDocumentsById))]
        public async Task<ActionResult<Document>> GetDocumentsById(string id)
        {
            var doc = await _docsService.GetAsync(id);

            if (doc is null)
            {
                return NotFound();
            }

            return doc;
        }

        [HttpPost]
        [ActionName(nameof(CreateDocument))]
        public async Task<IActionResult> CreateDocument(Document newDoc)
        {
            await _docsService.CreateAsync(newDoc);

            return CreatedAtAction(nameof(GetDocumentsById), new { id = newDoc.Id }, newDoc);
        }

        [HttpPut("{id:length(24)}")]
        [ActionName(nameof(UpdateDocument))]
        public async Task<IActionResult> UpdateDocument(string id, Document modDoc)
        {
            var doc = await _docsService.GetAsync(id);

            if (doc is null)
            {
                return NotFound();
            }

            modDoc.Id = doc.Id;

            await _docsService.UpdateAsync(id, modDoc);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ActionName(nameof(DeleteDocument))]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            var doc = await _docsService.GetAsync(id);

            if (doc is null)
            {
                return NotFound();
            }

            await _docsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
