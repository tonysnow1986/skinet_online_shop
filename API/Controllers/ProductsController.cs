using System;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class ProductsController(IGenericRepository<Product> repo) : BaseApiControllers
{ 
    
[HttpGet]
public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
{
    var spec = new ProductSpecification(specParams);
    
    return await CreatePageResult(repo, spec, specParams.PageIndex, specParams.PageSize);
}


[HttpGet("{id:int}")]
public async Task<ActionResult<Product>> GetProduct(int id)
{
    var product = await repo.GetByIdAsync(id);;

    if (product == null) return NotFound();
    
    return product;
}

[HttpPost]
public async Task<ActionResult<Product>> CreateProduct(Product product)
{
repo.Add(product);

 if (await repo.SaveChanges()) 
 {
     return CreatedAtAction("GetProduct", new { id = product.Id }, product);
 }

 return BadRequest("Problems with creating product");

   
}

[HttpPut("{id:int}")]
public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
{
if (product.Id != id || ! ProductExists(id)) 
    return BadRequest("Cannot update this product");

repo.Update(product);

if (await repo.SaveChanges())
{
    return NoContent();
}

return BadRequest("Problems with updating product");
}

[HttpDelete("{id:int}")]
public async Task<ActionResult<Product>> DeleteProduct(int id)
{
    var product = await repo.GetByIdAsync(id);

    if (product == null) return NotFound();
    
    repo.Remove(product);

    if (await repo.SaveChanges())
    {
        return NoContent();
    }

    return BadRequest("Problems with deleting product");
 
}
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {

        var spec = new TypeListSpecification();
        return Ok(await repo.ListAsync(spec));
    }

    private bool ProductExists(int id)
{
    return repo.Exist(id);
}

}