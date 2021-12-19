using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        //private readonly IProductRepository _repository;
        /*
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }
        */
        public ProductsController(IGenericRepository<Product> productRepository,
           IGenericRepository<ProductBrand> productBrandRepository,
           IGenericRepository<ProductType> productTypeRepository,
           IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductReturnDto>>> GetProducts(
          [FromQuery] ProductSpecParams productSpecParams
        )
        {
           // var products = await _repository.GetProductsAsync();
           //ISpecification<Product> spec =  new BaseSpecification<Product>();
           //spec.Includes.Add( x=> x.ProductBrand);
           //spec.Includes.Add( x=> x.ProductType);
           var spec = new ProductsWithTypesAndBrandsSpecification(productSpecParams);

           var countSpec = new ProductsWithFiltersForCountSpecification(productSpecParams);

           var totalItems = await _productRepository.CountAsync(countSpec);

           var products = await _productRepository.ListAsync(spec);

           var data = _mapper.Map<IReadOnlyList<ProductReturnDto>>(products);

            //var products = await _productRepository.ListAllAsync();
            return Ok(new Pagination<ProductReturnDto>(productSpecParams.PageIndex,
             productSpecParams.PageSize,totalItems,data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReturnDto>> GetProduct(int id)
        {
            //return await _repository.GetProductByIdAsync(id);
            //return await _productRepository.GetByIdAsync(id);
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec); 

             if (product == null) return NotFound(new ApiResponse(404));
             
            return _mapper.Map<ProductReturnDto>(product);
        }

        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            //var productBrands = await _repository.GetProductBrandsAsync();
            var productBrands = await _productBrandRepository.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            //var productTypes = await _repository.GetProductTypesAsync();
            var productTypes = await _productTypeRepository.ListAllAsync();
            return Ok(productTypes);
        }

    }
}