using H6_ChicBotique.Database.Entities;
using H6_ChicBotique.DTOs;
using H6_ChicBotique.Repositories;

namespace H6_ChicBotique.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse> GetProductById(int productId);
        Task<List<ProductResponse>> GetProductsByCategoryId(int categoryId);
        Task<ProductResponse> CreateProduct(ProductRequest newProduct);
        Task<ProductResponse> UpdateProduct(int ProductId, ProductRequest updateProduct);
        Task<ProductResponse> DeleteProduct(int productId);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            // Retrieve all products from the repository
            List<Product> products = await _productRepository.SelectAllProducts();

            // Map each product to a ProductResponse and return the list
            return products.Select(product => MapProductToProductResponse(product)).ToList();
        }

        public async Task<ProductResponse> GetProductById(int productId)
        {
            // Retrieve a product by ID from the repository
            Product product = await _productRepository.SelectProductById(productId);

            // If the product exists, map it to a ProductResponse and return
            if (product != null)
            {
                return MapProductToProductResponse(product);
            }

            return null;
        }

        public async Task<List<ProductResponse>> GetProductsByCategoryId(int categoryId)
        {
            // Retrieve products by category ID from the repository
            List<Product> products = await _productRepository.GetProductsByCategoryId(categoryId);

            // Map each product to a ProductResponse and return the list
            return products.Select(product => MapProductToProductResponse(product)).ToList();
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest newProduct)
        {
            // Map ProductRequest to a Product entity
            Product product = MapProductRequestToProduct(newProduct);

            // Insert the new product into the repository
            Product insertedProduct = await _productRepository.InsertNewProduct(product);

            // If the insertion is successful, map the inserted product to a ProductResponse
            if (insertedProduct != null)
            {
                // Retrieve the associated category for the product
                insertedProduct.Category = await _categoryRepository.SelectCategoryById(insertedProduct.CategoryId.GetValueOrDefault());

                // Map the product to a ProductResponse and return
                return MapProductToProductResponse(insertedProduct);
            }

            return null;
        }

        public async Task<ProductResponse> UpdateProduct(int productId, ProductRequest updateProduct)
        {
            // Map ProductRequest to a Product entity
            Product product = MapProductRequestToProduct(updateProduct);

            // Update the existing product in the repository
            Product updatedProduct = await _productRepository.UpdateExistingProduct(productId, product);

            // If the update is successful, map the updated product to a ProductResponse
            if (updatedProduct != null)
            {
                // Retrieve the associated category for the updated product
                updatedProduct.Category = await _categoryRepository.SelectCategoryById(updatedProduct.CategoryId.GetValueOrDefault());

                // Map the product to a ProductResponse and return
                return MapProductToProductResponse(updatedProduct);
            }

            return null;
        }

        public async Task<ProductResponse> DeleteProduct(int productId)
        {
            // Delete the product by ID from the repository
            Product product = await _productRepository.DeleteProductById(productId);

            // If the deletion is successful, map the deleted product to a ProductResponse
            if (product != null)
            {
                // Retrieve the associated category for the deleted product
                product.Category = await _categoryRepository.SelectCategoryById(product.CategoryId.GetValueOrDefault());

                // Map the product to a ProductResponse and return
                return MapProductToProductResponse(product);
            }

            return null;
        }

        // Helper method to map a Product to a ProductResponse
        private ProductResponse MapProductToProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Image = product.Image,
                Stock = product.Stock,
                CategoryId = product.Category.Id,
                Category = new ProductCategoryResponse
                {
                    Id = product.Category.Id,
                    CategoryName = product.Category.CategoryName
                }
            };
        }

        // Helper method to map a ProductRequest to a Product entity
        private static Product MapProductRequestToProduct(ProductRequest productRequest)
        {
            return new Product()
            {
                Title = productRequest.Title,
                Price = productRequest.Price,
                Description = productRequest.Description,
                Image = productRequest.Image,
                Stock = productRequest.Stock,
                CategoryId = productRequest.CategoryId
            };
        }
    }

}
