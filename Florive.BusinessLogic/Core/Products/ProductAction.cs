using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Core.Products
{
    public class ProductAction
    {
        protected static List<Product> _products = new List<Product>();

        protected ResponseMsg ExecuteGetAllProductsAction()
        {
            try
            {
                var result = new List<ProductDTO>();
                foreach (var product in _products)
                {
                    result.Add(new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Category = product.Category,
                        Image = product.Image
                    });
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Успешно получены продукты",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении продуктов: {ex.Message}"
                };
            }
        }

        protected ResponseMsg GetProductDataByIdAction(int id)
        {
            try
            {
                var product = _products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Продукт с ID {id} не найден"
                    };
                }

                var productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    Image = product.Image
                };

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт найден",
                    Data = productDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении продукта: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteProductCreateAction(ProductDTO product)
        {
            try
            {
                var newProduct = new Product
                {
                    Id = _products.Count + 1,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    Image = product.Image
                };

                _products.Add(newProduct);

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт создан успешно",
                    Data = newProduct
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при создании продукта: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteProductUpdateAction(int id, ProductDTO product)
        {
            try
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == id);

                if (existingProduct == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Продукт с ID {id} не найден"
                    };
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Category = product.Category;
                existingProduct.Image = product.Image;

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт обновлен успешно",
                    Data = existingProduct
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при обновлении продукта: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteProductDeleteAction(int id)
        {
            try
            {
                var product = _products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Продукт с ID {id} не найден"
                    };
                }

                _products.Remove(product);

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт удален успешно"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при удалении продукта: {ex.Message}"
                };
            }
        }
    }
}
