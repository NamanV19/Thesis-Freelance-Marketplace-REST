using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Data.Entities;
using Common.PostModels;
using Common.ViewModels;

namespace CatalogService.Helper
{
    public class EntityModelConverter
    {
        public static CatalogViewModel ConvertCatalogEntityToCatalogViewModel(Catalog theCatalog)
        => theCatalog == null ? new CatalogViewModel() : new CatalogViewModel
        {
            Id = theCatalog.Id,
            TypeOfWork = theCatalog.TypeOfWork,
            TitleOfJob  = theCatalog.TitleOfJob,
            JobDescription = theCatalog.JobDescription,
            JobCategory = theCatalog.JobCategory,
            ScopeOfWork = theCatalog.ScopeOfWork,
            EstimatedTime = theCatalog.EstimatedTime,
            Budget = theCatalog.Budget,
            Status = theCatalog.Status,
            dateCreated = theCatalog.dateCreated,
            BuyerId = theCatalog.BuyerId
        };

        public static CatalogPostModel ConvertCatalogEntityToCatalogPostModel(Catalog theCatalog)
        => theCatalog == null ? new CatalogPostModel() : new CatalogPostModel
        {
            TypeOfWork = theCatalog.TypeOfWork,
            TitleOfJob = theCatalog.TitleOfJob,
            JobDescription = theCatalog.JobDescription,
            JobCategory = theCatalog.JobCategory,
            ScopeOfWork = theCatalog.ScopeOfWork,
            EstimatedTime = theCatalog.EstimatedTime,
            Budget = theCatalog.Budget,
            Status = theCatalog.Status,
            dateCreated = theCatalog.dateCreated,
            BuyerId = theCatalog.BuyerId
        };

        public static Catalog ConvertCatalogPostModelToCatalogEntity(CatalogPostModel theCatalogModel)
        => theCatalogModel == null ? new Catalog() : new Catalog
        {
            TypeOfWork = theCatalogModel.TypeOfWork,
            TitleOfJob = theCatalogModel.TitleOfJob,
            JobDescription = theCatalogModel.JobDescription,
            JobCategory = theCatalogModel.JobCategory,
            ScopeOfWork = theCatalogModel.ScopeOfWork,
            EstimatedTime = theCatalogModel.EstimatedTime,
            Budget = theCatalogModel.Budget,
            Status = theCatalogModel.Status,
            dateCreated = theCatalogModel.dateCreated,
            BuyerId = theCatalogModel.BuyerId
        };
    }
}
