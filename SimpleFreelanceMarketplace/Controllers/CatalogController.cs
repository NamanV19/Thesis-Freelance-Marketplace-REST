using CatalogService.Data.Context;
using CatalogService.Data.Entities;
using CatalogService.Helper;
using Common.ViewModels;
using Common.PostModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public CatalogController(DatabaseContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<CatalogViewModel>> GetCatalogs() =>
            _context.Catalogs.Select(EntityModelConverter.ConvertCatalogEntityToCatalogViewModel).ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogViewModel>> GetCatalog(Guid id)
        {
            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog == null) { return NotFound($"A catalog with an id of {id} does not exist");  }
            return EntityModelConverter.ConvertCatalogEntityToCatalogViewModel(catalog);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CatalogPostModel>> PutCatalog(Guid id, CatalogPostModel catalog)
        {
            // if (id != catalog.Id) { return BadRequest($"Incorrect id: {id} specified for Catalog. "); }
            var catalogEntity = await _context.Catalogs.FindAsync(id);
            if (catalogEntity == null) { return NotFound(); }
            else
            {
                catalogEntity.TypeOfWork = catalog.TypeOfWork;
                catalogEntity.TitleOfJob = catalog.TitleOfJob;
                catalogEntity.JobDescription = catalog.JobDescription;
                catalogEntity.JobCategory = catalog.JobCategory;
                catalogEntity.ScopeOfWork = catalog.ScopeOfWork;
                catalogEntity.EstimatedTime = catalog.EstimatedTime;
                catalogEntity.Budget = catalog.Budget;
                catalogEntity.Status = catalog.Status;
                catalogEntity.dateCreated = catalog.dateCreated;
                catalogEntity.BuyerId = catalog.BuyerId;

                _context.Entry(catalogEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(EntityModelConverter.ConvertCatalogEntityToCatalogViewModel(catalogEntity));
        }

        [HttpPost]
        public async Task<ActionResult<CatalogPostModel>> PostCatalog(CatalogPostModel catalog)
        {
            var catalogEntity = EntityModelConverter.ConvertCatalogPostModelToCatalogEntity(catalog);

            _context.Catalogs.Add(catalogEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostCatalog", new { id = catalogEntity.Id }, EntityModelConverter.ConvertCatalogEntityToCatalogViewModel(catalogEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CatalogViewModel>> DeleteCatalog(Guid id)
        {
            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog == null) { return NotFound(); }
            _context.Catalogs.Remove(catalog);

            try { await _context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                return BadRequest($"Exception deleting Catalog: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return EntityModelConverter.ConvertCatalogEntityToCatalogViewModel(catalog);
        }
    }
}
